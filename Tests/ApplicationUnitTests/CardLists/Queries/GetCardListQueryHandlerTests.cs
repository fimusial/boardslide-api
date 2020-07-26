using System.Threading;
using System.Threading.Tasks;
using BoardSlide.API.Application.CardLists.Queries.GetCardList;
using BoardSlide.API.Application.CardLists.Responses;
using BoardSlide.API.Application.Common.Exceptions;
using BoardSlide.API.ApplicationUnitTests.Common;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace BoardSlide.API.ApplicationUnitTests.CardLists.Queries
{
    public class GetCardListQueryHandlerTests : RequestHandlerTestBase
    {
        [Fact]
        public async Task Handle_ShouldReturnCardList_WhenRequestIsValid()
        {
            var userId = "1";
            var expectedBoardId = 1;
            var expectedCardListId = 11;

            var sut = new GetCardListQueryHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var query = new GetCardListQuery()
            {
                BoardId = expectedBoardId,
                CardListId = expectedCardListId
            };

            var result = await sut.Handle(query, CancellationToken.None);
            result.ShouldNotBeNull();
            result.Id.ShouldBe(expectedCardListId);
            result.BoardId.ShouldBe(expectedBoardId);

            var dbCardList = _mapper.Map<CardListResponse>(await
                _unitOfWork.CardLists.GetCardListWithBoardIdAsync(result.Id, result.BoardId, true));

            var resultSerialized = JsonConvert.SerializeObject(result);
            var dbCardListSerialized = JsonConvert.SerializeObject(dbCardList);
            resultSerialized.ShouldBe(dbCardListSerialized);
        }

        [Theory]
        [InlineData(100, 1000)]
        [InlineData(1, 1000)]
        [InlineData(100, 11)]
        [InlineData(1, 21)]
        public async Task Handle_ShouldThrowNotFoundException_WhenResourceDoesNotExist(int boardId, int cardListId)
        {
            var userId = "1";

            var sut = new GetCardListQueryHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var query = new GetCardListQuery()
            {
                BoardId = boardId,
                CardListId = cardListId
            };

            var result = await sut.Handle(query, CancellationToken.None).ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task Handle_ShouldThrowUnauthorizedException_WhenBoardDoesNotBelongToUser()
        {
            var boardId = 1;
            var userId = "2";

            var sut = new GetCardListQueryHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var query = new GetCardListQuery()
            {
                BoardId = boardId
            };

            await sut.Handle(query, CancellationToken.None).ShouldThrowAsync<UnauthorizedException>();
        }
    }
}