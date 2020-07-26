using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BoardSlide.API.Application.CardLists.Queries.GetCardLists;
using BoardSlide.API.Application.CardLists.Responses;
using BoardSlide.API.Application.Common.Exceptions;
using BoardSlide.API.ApplicationUnitTests.Common;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace BoardSlide.API.ApplicationUnitTests.CardLists.Queries
{
    public class GetCardListsQueryHandlerTests : RequestHandlerTestBase
    {
        [Fact]
        public async Task Handle_ShouldReturnCardLists_WhenRequestIsValid()
        {
            var userId = "1";
            var boardId = 1;
            var expectedCardListCount = 2;

            var sut = new GetCardListsQueryHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var query = new GetCardListsQuery()
            {
                BoardId = boardId
            };

            var result = await sut.Handle(query, CancellationToken.None);
            result.ShouldNotBeNull();
            result.Count().ShouldBe(expectedCardListCount);

            var dbCardLists = _mapper.Map<IEnumerable<CardListInfoResponse>>(
                await _unitOfWork.CardLists.FindAsync(list => list.BoardId == boardId));

            var resultSerialized = JsonConvert.SerializeObject(result);
            var dbCardListSerialized = JsonConvert.SerializeObject(dbCardLists);
            resultSerialized.ShouldBe(dbCardListSerialized);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenBoardHasNoCardLists()
        {
            var userId = "2";
            var boardId = 3;

            var sut = new GetCardListsQueryHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var query = new GetCardListsQuery()
            {
                BoardId = boardId
            };

            var result = await sut.Handle(query, CancellationToken.None);
            result.ShouldNotBeNull();
            result.Count().ShouldBe(0);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenBoardDoesNotExist()
        {
            var userId = "1";
            var boardId = 100;

            var sut = new GetCardListsQueryHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var query = new GetCardListsQuery()
            {
                BoardId = boardId
            };

            await sut.Handle(query, CancellationToken.None).ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task Handle_ShouldThrowUnauthorizedException_WhenBoardDoesNotBelongToUser()
        {
            var userId = "2";
            var boardId = 1;

            var sut = new GetCardListsQueryHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var query = new GetCardListsQuery()
            {
                BoardId = boardId
            };

            await sut.Handle(query, CancellationToken.None).ShouldThrowAsync<UnauthorizedException>();
        }
    }
}