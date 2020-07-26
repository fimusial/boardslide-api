using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BoardSlide.API.Application.Cards.Queries.GetCards;
using BoardSlide.API.Application.Cards.Responses;
using BoardSlide.API.Application.Common.Exceptions;
using BoardSlide.API.ApplicationUnitTests.Common;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace BoardSlide.API.ApplicationUnitTests.Cards.Queries
{
    public class GetCardsQueryHandlerTests : RequestHandlerTestBase
    {
        [Fact]
        public async Task Handle_ShouldReturnCards_WhenRequestIsValid()
        {
            var userId = "1";
            var boardId = 1;
            var cardListId = 11;

            var sut = new GetCardsQueryHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var query = new GetCardsQuery()
            {
                BoardId = boardId,
                CardListId = cardListId
            };

            var result = await sut.Handle(query, CancellationToken.None);
            result.ShouldNotBeNull();
            result.Count().ShouldBe(2);

            var dbCards = _mapper.Map<IEnumerable<CardResponse>>(
                await _unitOfWork.Cards.FindAsync(card => card.CardListId == cardListId));

            var resultSerialized = JsonConvert.SerializeObject(result);
            var dbCardsSerialized = JsonConvert.SerializeObject(dbCards);
            resultSerialized.ShouldBe(dbCardsSerialized);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenCardListHasNoCards()
        {
            var userId = "2";
            var boardId = 2;
            var cardListId = 23;

            var sut = new GetCardsQueryHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var query = new GetCardsQuery()
            {
                BoardId = boardId,
                CardListId = cardListId
            };

            var result = await sut.Handle(query, CancellationToken.None);
            result.ShouldNotBeNull();
            result.Count().ShouldBe(0);
        }

        [Theory]
        [InlineData(1000, 1000)]
        [InlineData(1, 1000)]
        [InlineData(1000, 11)]
        [InlineData(1, 22)]
        public async Task Handle_ShouldThrowNotFoundException_WhenResourceDoesNotExist(int boardId, int cardListId)
        {
            var userId = "1";

            var sut = new GetCardsQueryHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);

            var query = new GetCardsQuery()
            {
                BoardId = boardId,
                CardListId = cardListId
            };

            var result = await sut.Handle(query, CancellationToken.None).ShouldThrowAsync<NotFoundException>();
        }
        
        [Fact]
        public async Task Handle_ShouldThrowUnauthorizedException_WhenBoardDoesNotBelongToUser()
        {
            var userId = "100";
            var boardId = 1;
            var cardListId = 11;

            var sut = new GetCardsQueryHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);

            var query = new GetCardsQuery()
            {
                BoardId = boardId,
                CardListId = cardListId
            };

            var result = await sut.Handle(query, CancellationToken.None).ShouldThrowAsync<UnauthorizedException>();
        }
    }
}