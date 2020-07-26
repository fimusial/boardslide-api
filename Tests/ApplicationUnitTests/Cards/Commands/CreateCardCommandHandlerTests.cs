using System;
using System.Threading;
using System.Threading.Tasks;
using BoardSlide.API.Application.Cards.Commands.CreateCard;
using BoardSlide.API.Application.Common.Exceptions;
using BoardSlide.API.ApplicationUnitTests.Common;
using Shouldly;
using Xunit;

namespace BoardSlide.API.ApplicationUnitTests.Cards.Commands
{
    public class CreateCardCommandHandlerTests : RequestHandlerTestBase
    {
        [Fact]
        public async Task Handle_ShouldCreateCard_WhenRequestIsValid()
        {
            var userId = "1";
            var expectedBoardId = 1;
            var expectedCardListId = 11;

            var expectedCardName = "card-name";
            var expectedDescription = "desc";
            var expectedDueDate = DateTime.Parse("2020-07-27");

            var sut = new CreateCardCommandHandler(GetCurrentUserMock(userId),
                _unitOfWork, _mapper, GetApplicationSettingsMock(10, 10));

            var command = new CreateCardCommand()
            {
                BoardId = expectedBoardId,
                CardListId = expectedCardListId,
                Name = expectedCardName,
                Description = expectedDescription,
                DueDate = expectedDueDate
            };

            var result = await sut.Handle(command, CancellationToken.None);
            result.ShouldNotBeNull();
            result.CardListId.ShouldBe(expectedCardListId);
            result.Name.ShouldBe(expectedCardName);
            result.Description.ShouldBe(expectedDescription);
            result.DueDate.ShouldBe(expectedDueDate);

            var dbCard = await _unitOfWork.Cards.GetAsync(result.Id);
            dbCard.Id.ShouldBe(result.Id);
            dbCard.CardListId.ShouldBe(result.CardListId);
            dbCard.Name.ShouldBe(result.Name);
            dbCard.Description.ShouldBe(result.Description);
            dbCard.DueDate.ShouldBe(result.DueDate);
        }

        [Fact]
        public async Task Handle_ShouldThrowBadRequestException_WhenCardLimitForCardListIsExceeded()
        {
            var userId = "1";
            var boardId = 1;
            var cardListId = 11;
            var cardName = "card-name";

            var sut = new CreateCardCommandHandler(GetCurrentUserMock(userId),
                _unitOfWork, _mapper, GetApplicationSettingsMock(1, 1));

            var command = new CreateCardCommand()
            {
                BoardId = boardId,
                CardListId = cardListId,
                Name = cardName
            };

            var result = await sut.Handle(command, CancellationToken.None).ShouldThrowAsync<BadRequestException>();
        }

        [Theory]
        [InlineData(1000, 1000)]
        [InlineData(1, 1000)]
        [InlineData(1000, 11)]
        [InlineData(1, 22)]
        public async Task Handle_ShouldThrowNotFoundException_WhenResourceDoesNotExist(int boardId, int cardListId)
        {
            var userId = "1";
            var cardName = "card-name";

            var sut = new CreateCardCommandHandler(GetCurrentUserMock(userId),
                _unitOfWork, _mapper, GetApplicationSettingsMock(10, 10));

            var command = new CreateCardCommand()
            {
                BoardId = boardId,
                CardListId = cardListId,
                Name = cardName
            };

            var result = await sut.Handle(command, CancellationToken.None).ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task Handle_ShouldThrowUnauthorizedException_WhenBoardDoesNotBelongToUser()
        {
            var userId = "100";
            var boardId = 1;
            var cardListId = 11;
            var cardName = "card-name";

            var sut = new CreateCardCommandHandler(GetCurrentUserMock(userId),
                _unitOfWork, _mapper, GetApplicationSettingsMock(10, 10));

            var command = new CreateCardCommand()
            {
                BoardId = boardId,
                CardListId = cardListId,
                Name = cardName
            };

            var result = await sut.Handle(command, CancellationToken.None).ShouldThrowAsync<UnauthorizedException>();
        }
    }
}