using System.Threading;
using System.Threading.Tasks;
using BoardSlide.API.Application.Boards.Queries.GetBoard;
using BoardSlide.API.Application.Boards.Responses;
using BoardSlide.API.Application.Common.Exceptions;
using BoardSlide.API.ApplicationUnitTests.Common;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace BoardSlide.API.ApplicationUnitTests.Boards.Queries
{
    public class GetBoardQueryHandlerTests : RequestHandlerTestBase
    {
        [Fact]
        public async Task Handle_ShouldReturnBoard_WhenRequestIsValid()
        {
            var userId = "1";
            var expectedBoardId = 1;

            var sut = new GetBoardQueryHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var query = new GetBoardQuery()
            {
                BoardId = expectedBoardId
            };

            var result = await sut.Handle(query, CancellationToken.None);
            result.ShouldNotBeNull();
            result.Id.ShouldBe(expectedBoardId);

            var dbBoard = _mapper.Map<BoardResponse>(await _unitOfWork.Boards.GetBoardAsync(expectedBoardId, true, true));

            var resultSerialized = JsonConvert.SerializeObject(result);
            var dbBoardSerialized = JsonConvert.SerializeObject(dbBoard);
            resultSerialized.ShouldBe(dbBoardSerialized);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenBoardDoesNotExist()
        {
            var userId = "1";
            var boardId = 100;

            var sut = new GetBoardQueryHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var query = new GetBoardQuery()
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

            var sut = new GetBoardQueryHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var query = new GetBoardQuery()
            {
                BoardId = boardId
            };

            await sut.Handle(query, CancellationToken.None).ShouldThrowAsync<UnauthorizedException>();
        }
    }
}