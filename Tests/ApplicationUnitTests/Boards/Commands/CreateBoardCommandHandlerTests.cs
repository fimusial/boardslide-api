using System.Threading;
using System.Threading.Tasks;
using BoardSlide.API.Application.Boards.Commands.CreateBoard;
using BoardSlide.API.ApplicationUnitTests.Common;
using Shouldly;
using Xunit;

namespace BoardSlide.API.ApplicationUnitTests.Boards.Commands
{
    public class CreateBoardCommandHandlerTests : RequestHandlerTestBase
    {
        [Fact]
        public async Task Handle_ShouldCreateBoard_WhenRequestIsValid()
        {
            var expectedUserId = "1";
            var expectedBoardName = "board-name";

            var sut = new CreateBoardCommandHandler(GetCurrentUserMock(expectedUserId), _unitOfWork, _mapper);
            var command = new CreateBoardCommand()
            {
                Name = expectedBoardName
            };

            var result = await sut.Handle(command, CancellationToken.None);
            result.ShouldNotBeNull();
            result.Name.ShouldBe(expectedBoardName);

            var dbBoard = await _unitOfWork.Boards.GetAsync(result.Id);
            dbBoard.Id.ShouldBe(result.Id);
            dbBoard.Name.ShouldBe(result.Name);
            dbBoard.OwnerId.ShouldBe(expectedUserId);
        }
    }
}