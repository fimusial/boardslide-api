using System.Threading;
using System.Threading.Tasks;
using BoardSlide.API.Application.CardLists.Commands.DeleteCardList;
using BoardSlide.API.Application.Common.Exceptions;
using BoardSlide.API.ApplicationUnitTests.Common;
using Shouldly;
using Xunit;

namespace BoardSlide.API.ApplicationUnitTests.CardLists.Commands
{
    public class DeleteCardListCommandHandlerTests : RequestHandlerTestBase
    {
        [Fact]
        public async Task Handle_ShouldDeleteCardList_WhenRequestIsValid()
        {
            var userId = "1";
            var boardId = 1;
            var cardListId = 11;

            var sut = new DeleteCardListCommandHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var command = new DeleteCardListCommand()
            {
                BoardId = boardId,
                CardListId = cardListId
            };

            await sut.Handle(command, CancellationToken.None);
            
            (await _unitOfWork.CardLists.GetAsync(boardId)).ShouldBeNull();
        }

        [Theory]
        [InlineData(1000, 1000)]
        [InlineData(1, 1000)]
        [InlineData(100, 11)]
        [InlineData(1, 21)]
        public async Task Handle_ShouldThrowNotFoundException_WhenResourceDoesNotExist(int boardId, int cardListId)
        {
            var userId = "1";

            var sut = new DeleteCardListCommandHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var command = new DeleteCardListCommand()
            {
                BoardId = boardId,
                CardListId = cardListId
            };

            var result = await sut.Handle(command, CancellationToken.None).ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task Handle_ShouldThrowUnauthorizedException_WhenBoardDoesNotBelongToUser()
        {
            var userId = "2";
            var boardId = 1;
            var cardListId = 11;

            var sut = new DeleteCardListCommandHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var command = new DeleteCardListCommand()
            {
                BoardId = boardId,
                CardListId = cardListId
            };

            var result = await sut.Handle(command, CancellationToken.None).ShouldThrowAsync<UnauthorizedException>();
        }
    }
}