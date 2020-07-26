using System;
using System.Threading;
using System.Threading.Tasks;
using BoardSlide.API.Application.Cards.Commands.UpdateCard;
using BoardSlide.API.Application.Common.Exceptions;
using BoardSlide.API.ApplicationUnitTests.Common;
using Shouldly;
using Xunit;

namespace BoardSlide.API.ApplicationUnitTests.Cards.Commands
{
    public class UpdateCardCommandHandlerTests : RequestHandlerTestBase
    {
        [Fact]
        public async Task Handle_ShouldUpdateCard_WhenRequestIsValid()
        {
            var userId = "1";
            var expectedBoardId = 1;
            var expectedCardListId = 11;
            var expectedCardId = 111;

            var expectedCardName = "new-card-name";
            var expectedDescription = "desc";
            var expectedDueDate = DateTime.Parse("7171-01-01");

            var sut = new UpdateCardCommandHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var command = new UpdateCardCommand()
            {
                BoardId = expectedBoardId,
                CardListId = expectedCardListId,
                NewCardListId = null,
                CardId = expectedCardId,
                Name = expectedCardName,
                Description = expectedDescription,
                DueDate = expectedDueDate
            };

            var result = await sut.Handle(command, CancellationToken.None);
            result.ShouldNotBeNull();
            result.Id.ShouldBe(expectedCardId);
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
        public async Task Handle_ShouldUpdateCardListId_WhenCardListBelongsToTheSameBoard()
        {
            var userId = "1";
            var boardId = 1;
            var cardListId = 11;
            var cardId = 111;

            var expectedNewCardListId = 12;
            var sut = new UpdateCardCommandHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var command = new UpdateCardCommand()
            {
                BoardId = boardId,
                CardListId = cardListId,
                NewCardListId = expectedNewCardListId,
                CardId = cardId
            };

            var result = await sut.Handle(command, CancellationToken.None);
            result.CardListId.ShouldBe(expectedNewCardListId);
        }

        [Fact]
        public async Task Handle_ShouldThrowBadRequestException_WhenCardListDoesNotBelongToTheSameBoard()
        {
            var userId = "1";
            var boardId = 1;
            var cardListId = 11;
            var cardId = 111;

            var expectedNewCardListId = 22;
            var sut = new UpdateCardCommandHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var command = new UpdateCardCommand()
            {
                BoardId = boardId,
                CardListId = cardListId,
                NewCardListId = expectedNewCardListId,
                CardId = cardId
            };

            var result = await sut.Handle(command, CancellationToken.None).ShouldThrowAsync<BadRequestException>();
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

            var sut = new UpdateCardCommandHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var command = new UpdateCardCommand()
            {
                BoardId = boardId,
                CardListId = cardListId,
                CardId = cardId
            };

            var result = await sut.Handle(command, CancellationToken.None).ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task Handle_ShouldThrowUnauthorizedException_WhenBoardDoesNotBelongToUser()
        {
            var userId = "100";
            var boardId = 1;
            var cardListId = 11;
            var cardId = 111;

            var sut = new UpdateCardCommandHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var command = new UpdateCardCommand()
            {
                BoardId = boardId,
                CardListId = cardListId,
                CardId = cardId
            };

            var result = await sut.Handle(command, CancellationToken.None).ShouldThrowAsync<UnauthorizedException>();
        }
    }
}