using System.Threading;
using System.Threading.Tasks;
using BoardSlide.API.Application.Boards.Commands.DeleteBoard;
using BoardSlide.API.Application.Common.Exceptions;
using BoardSlide.API.ApplicationUnitTests.Common;
using Shouldly;
using Xunit;

namespace BoardSlide.API.ApplicationUnitTests.Boards.Commands
{
    public class DeleteBoardCommandHandlerTests : RequestHandlerTestBase
    {
        [Fact]
        public async Task Handle_ShouldDeleteBoard_WhenRequestIsValid()
        {
            var userId = "1";
            var expectedBoardId = 1;

            var sut = new DeleteBoardCommandHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var command = new DeleteBoardCommand()
            {
                BoardId = expectedBoardId
            };

            await sut.Handle(command, CancellationToken.None);
            
            (await _unitOfWork.Boards.GetAsync(expectedBoardId)).ShouldBeNull();
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenBoardDoesNotExist()
        {
            var userId = "1";
            var boardId = 71;

            var sut = new DeleteBoardCommandHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var command = new DeleteBoardCommand()
            {
                BoardId = boardId
            };

            var result = await sut.Handle(command, CancellationToken.None).ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task Handle_ShouldThrowUnauthorizedException_WhenBoardDoesNotBelongToUser()
        {
            var userId = "2";
            var boardId = 1;

            var sut = new DeleteBoardCommandHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var command = new DeleteBoardCommand()
            {
                BoardId = boardId
            };

            var result = await sut.Handle(command, CancellationToken.None).ShouldThrowAsync<UnauthorizedException>();
        }
    }
}