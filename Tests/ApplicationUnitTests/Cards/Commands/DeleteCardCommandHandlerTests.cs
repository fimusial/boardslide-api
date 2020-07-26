using System.Threading;
using System.Threading.Tasks;
using BoardSlide.API.Application.Cards.Commands.DeleteCard;
using BoardSlide.API.Application.Common.Exceptions;
using BoardSlide.API.ApplicationUnitTests.Common;
using Shouldly;
using Xunit;

namespace BoardSlide.API.ApplicationUnitTests.Cards.Commands
{
    public class DeleteCardCommandHandlerTests : RequestHandlerTestBase
    {
        [Fact]
        public async Task Handle_ShouldDeleteCard_WhenRequestIsValid()
        {
            var userId = "1";
            var boardId = 1;
            var cardListId = 11;
            var cardId = 111;

            var sut = new DeleteCardCommandHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var command = new DeleteCardCommand()
            {
                BoardId = boardId,
                CardListId = cardListId,
                CardId = cardId
            };

            await sut.Handle(command, CancellationToken.None);
            
            (await _unitOfWork.Cards.GetAsync(boardId)).ShouldBeNull();
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

            var sut = new DeleteCardCommandHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var command = new DeleteCardCommand()
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

            var sut = new DeleteCardCommandHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var command = new DeleteCardCommand()
            {
                BoardId = boardId,
                CardListId = cardListId,
                CardId = cardId
            };

            var result = await sut.Handle(command, CancellationToken.None).ShouldThrowAsync<UnauthorizedException>();
        }
    }
}