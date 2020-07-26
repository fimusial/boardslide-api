using System.Threading;
using System.Threading.Tasks;
using BoardSlide.API.Application.Boards.Commands.UpdateBoard;
using BoardSlide.API.Application.Common.Exceptions;
using BoardSlide.API.ApplicationUnitTests.Common;
using Shouldly;
using Xunit;

namespace BoardSlide.API.ApplicationUnitTests.Boards.Commands
{
    public class UpdateBoardCommandHandlerTests : RequestHandlerTestBase
    {
        [Fact]
        public async Task Handle_ShouldUpdateBoard_WhenRequestIsValid()
        {
            var expectedUserId = "1";
            var expectedBoardId = 1;
            var expectedBoardName = "new-board-name";

            var sut = new UpdateBoardCommandHandler(GetCurrentUserMock(expectedUserId), _unitOfWork, _mapper);
            var command = new UpdateBoardCommand()
            {
                BoardId = expectedBoardId,
                Name = expectedBoardName
            };

            var response = await sut.Handle(command, CancellationToken.None);
            response.ShouldNotBeNull();
            response.Id.ShouldBe(expectedBoardId);
            response.Name.ShouldBe(expectedBoardName);
            
            var dbBoard = await _unitOfWork.Boards.GetAsync(expectedBoardId);
            dbBoard.Id.ShouldBe(response.Id);
            dbBoard.Name.ShouldBe(response.Name);
            dbBoard.OwnerId.ShouldBe(expectedUserId);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenBoardDoesNotExist()
        {
            var userId = "1";
            var boardId = 71;
            var boardName = "new-board-name";

            var sut = new UpdateBoardCommandHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var command = new UpdateBoardCommand()
            {
                BoardId = boardId,
                Name = boardName
            };

            var result = await sut.Handle(command, CancellationToken.None).ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task Handle_ShouldThrowUnauthorizedException_WhenBoardDoesNotBelongToUser()
        {
            var userId = "2";
            var boardId = 1;
            var boardName = "new-board-name";

            var sut = new UpdateBoardCommandHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var command = new UpdateBoardCommand()
            {
                BoardId = boardId,
                Name = boardName
            };

            var result = await sut.Handle(command, CancellationToken.None).ShouldThrowAsync<UnauthorizedException>();
        }
    }
}