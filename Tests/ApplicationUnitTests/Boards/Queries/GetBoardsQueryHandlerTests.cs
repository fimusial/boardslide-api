using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BoardSlide.API.Application.Boards.Queries.GetBoards;
using BoardSlide.API.Application.Boards.Responses;
using BoardSlide.API.ApplicationUnitTests.Common;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace BoardSlide.API.ApplicationUnitTests.Boards.Queries
{
    public class GetBoardsQueryHandlerTests : RequestHandlerTestBase
    {
        [Fact]
        public async Task Handle_ShouldReturnBoards()
        {
            var userId = "1";
            var expectedBoardCount = 1;

            var sut = new GetBoardsQueryHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var query = new GetBoardsQuery();

            var result = await sut.Handle(query, CancellationToken.None);
            result.ShouldNotBeNull();
            result.Count().ShouldBe(expectedBoardCount);

            var dbBoards = _mapper.Map<IEnumerable<BoardInfoResponse>>(
                await _unitOfWork.Boards.FindAsync(board => board.OwnerId == userId));

            var resultSerialized = JsonConvert.SerializeObject(result);
            var dbBoardSerialized = JsonConvert.SerializeObject(dbBoards);
            resultSerialized.ShouldBe(dbBoardSerialized);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenUserHasNoBoards()
        {
            var userId = "100";

            var sut = new GetBoardsQueryHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var query = new GetBoardsQuery();

            var result = await sut.Handle(query, CancellationToken.None);
            result.ShouldNotBeNull();
            result.Count().ShouldBe(0);
        }
    }
}