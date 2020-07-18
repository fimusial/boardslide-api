using MediatR;

namespace BoardSlide.API.Application.CardLists.Commands.DeleteCardList
{
    public class DeleteCardListCommand : IRequest
    {
        public int BoardId { get; set; }
        public int CardListId { get; set; }
    }
}