using System.Threading;
using System.Threading.Tasks;
using BoardSlide.API.Application.Cards.Queries.GetCard;
using BoardSlide.API.Application.Cards.Responses;
using BoardSlide.API.Application.Common.Exceptions;
using BoardSlide.API.ApplicationUnitTests.Common;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace ApplicationUnitTests.Cards.Queries
{
    public class GetCardQueryHandlerTests : RequestHandlerTestBase
    {
        [Fact]
        public async Task Handle_ShouldReturnCard_WhenRequestIsValid()
        {
            var userId = "1";
            var expectedBoardId = 1;
            var expectedCardListId = 11;
            var expectedCardId = 111;

            var sut = new GetCardQueryHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var query = new GetCardQuery()
            {
                BoardId = expectedBoardId,
                CardListId = expectedCardListId,
                CardId = expectedCardId
            };

            var result = await sut.Handle(query, CancellationToken.None);
            result.ShouldNotBeNull();
            result.Id.ShouldBe(expectedCardId);
            result.CardListId.ShouldBe(expectedCardListId);

            var dbCard = _mapper.Map<CardResponse>(await
                _unitOfWork.Cards.GetCardWithCardListIdAndBoardIdAsync(expectedCardId, expectedCardListId, expectedBoardId));

            var resultSerialized = JsonConvert.SerializeObject(result);
            var dbCardSerialized = JsonConvert.SerializeObject(dbCard);
            resultSerialized.ShouldBe(dbCardSerialized);
        }

        [Theory]
        [InlineData(1000, 1000, 1000)]
        [InlineData(1000, 11, 111)]
        [InlineData(1, 1000, 111)]
        [InlineData(1, 11, 1000)]
        [InlineData(1, 22, 111)]
        [InlineData(1, 11, 222)]
        public async Task Handle_ShouldThrowNotFoundException_WhenResourceDoesNotExist(int boardId, int cardListId, int cardId)
        {
            var userId = "1";

            var sut = new GetCardQueryHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);

            var query = new GetCardQuery()
            {
                BoardId = boardId,
                CardListId = cardListId,
                CardId = cardId
            };

            var result = await sut.Handle(query, CancellationToken.None).ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task Handle_ShouldThrowUnauthorizedException_WhenBoardDoesNotBelongToUser()
        {
            var userId = "100";
            var boardId = 1;
            var cardListId = 11;
            var cardId = 111;

            var sut = new GetCardQueryHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);

            var query = new GetCardQuery()
            {
                BoardId = boardId,
                CardListId = cardListId,
                CardId = cardId
            };

            var result = await sut.Handle(query, CancellationToken.None).ShouldThrowAsync<UnauthorizedException>();
        }
    }
}