using System.Threading;
using System.Threading.Tasks;
using BoardSlide.API.Application.CardLists.Commands.CreateCardList;
using BoardSlide.API.Application.Common.Exceptions;
using BoardSlide.API.ApplicationUnitTests.Common;
using Shouldly;
using Xunit;

namespace BoardSlide.API.ApplicationUnitTests.CardLists.Commands
{
    public class CreateCardListCommandHandlerTests : RequestHandlerTestBase
    {
        [Fact]
        public async Task Handle_ShouldCreateCardList_WhenRequestIsValid_And_CardListLimitIsNotExceeded()
        {
            var userId = "1";
            var expectedBoardId = 1;
            var expectedCardListName = "card-list-name";

            var sut = new CreateCardListCommandHandler(GetCurrentUserMock(userId),
                _unitOfWork, _mapper, GetApplicationSettingsMock(10, 10));

            var command = new CreateCardListCommand()
            {
                BoardId = expectedBoardId,
                Name = expectedCardListName
            };

            var result = await sut.Handle(command, CancellationToken.None);
            result.ShouldNotBeNull();
            result.BoardId.ShouldBe(expectedBoardId);
            result.Name.ShouldBe(expectedCardListName);

            var dbCardList = await _unitOfWork.CardLists.GetAsync(result.Id);
            dbCardList.Id.ShouldBe(result.Id);
            dbCardList.BoardId.ShouldBe(result.BoardId);
            dbCardList.Name.ShouldBe(result.Name);
        }

        [Fact]
        public async Task Handle_ShouldThrowBadRequestException_WhenCardListLimitForBoardIsExceeded()
        {
            var userId = "1";
            var boardId = 1;
            var cardListName = "card-list-name";

            var sut = new CreateCardListCommandHandler(GetCurrentUserMock(userId),
                _unitOfWork, _mapper, GetApplicationSettingsMock(1, 1));

            var command = new CreateCardListCommand()
            {
                BoardId = boardId,
                Name = cardListName
            };

            var result = await sut.Handle(command, CancellationToken.None).ShouldThrowAsync<BadRequestException>();
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenBoardDoesNotExist()
        {
            var userId = "1";
            var boardId = 100;
            var cardListName = "card-list-name";

            var sut = new CreateCardListCommandHandler(GetCurrentUserMock(userId),
                _unitOfWork, _mapper, GetApplicationSettingsMock(10, 10));

            var command = new CreateCardListCommand()
            {
                BoardId = boardId,
                Name = cardListName
            };

            var result = await sut.Handle(command, CancellationToken.None).ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task Handle_ShouldThrowUnauthorizedException_WhenBoardDoesNotBelongToUser()
        {
            var userId = "100";
            var boardId = 1;
            var cardListName = "card-list-name";

            var sut = new CreateCardListCommandHandler(GetCurrentUserMock(userId),
                _unitOfWork, _mapper, GetApplicationSettingsMock(10, 10));

            var command = new CreateCardListCommand()
            {
                BoardId = boardId,
                Name = cardListName
            };

            var result = await sut.Handle(command, CancellationToken.None).ShouldThrowAsync<UnauthorizedException>();
        }
    }
}