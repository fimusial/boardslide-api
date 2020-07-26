using System.Threading;
using System.Threading.Tasks;
using BoardSlide.API.Application.CardLists.Commands.UpdateCardList;
using BoardSlide.API.Application.Common.Exceptions;
using BoardSlide.API.ApplicationUnitTests.Common;
using Shouldly;
using Xunit;

namespace BoardSlide.API.ApplicationUnitTests.CardLists.Commands
{
    public class UpdateCardListCommandHandlerTests : RequestHandlerTestBase
    {
        [Fact]
        public async Task Handle_ShouldUpdateCardList_WhenRequestIsValid()
        {
            var userId = "1";
            var expectedBoardId = 1;
            var expectedCardListId = 11;
            var expectedCardListName = "new-card-list-name";

            var sut = new UpdateCardListCommandHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var command = new UpdateCardListCommand()
            {
                BoardId = expectedBoardId,
                CardListId = expectedCardListId,
                Name = expectedCardListName
            };

            var result = await sut.Handle(command, CancellationToken.None);
            result.ShouldNotBeNull();
            result.Id.ShouldBe(expectedCardListId);
            result.BoardId.ShouldBe(expectedBoardId);
            result.Name.ShouldBe(expectedCardListName);

            var dbCardList = await _unitOfWork.CardLists.GetAsync(result.Id);
            dbCardList.Id.ShouldBe(result.Id);
            dbCardList.BoardId.ShouldBe(result.BoardId);
            dbCardList.Name.ShouldBe(result.Name);
        }


        [Theory]
        [InlineData(1000, 1000)]
        [InlineData(1, 1000)]
        [InlineData(100, 11)]
        [InlineData(1, 21)]
        public async Task Handle_ShouldThrowNotFoundException_WhenResourceDoesNotExist(int boardId, int cardListId)
        {
            var userId = "1";
            var cardListName = "new-card-list-name";

            var sut = new UpdateCardListCommandHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var command = new UpdateCardListCommand()
            {
                BoardId = boardId,
                CardListId = cardListId,
                Name = cardListName
            };

            var result = await sut.Handle(command, CancellationToken.None).ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task Handle_ShouldThrowUnauthorizedException_WhenBoardDoesNotBelongToUser()
        {
            var userId = "100";
            var boardId = 1;
            var cardListId = 11;
            var cardListName = "new-card-list-name";

            var sut = new UpdateCardListCommandHandler(GetCurrentUserMock(userId), _unitOfWork, _mapper);
            var command = new UpdateCardListCommand()
            {
                BoardId = boardId,
                CardListId = cardListId,
                Name = cardListName
            };

            var result = await sut.Handle(command, CancellationToken.None).ShouldThrowAsync<UnauthorizedException>();
        }
    }
}