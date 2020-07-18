using MediatR;

namespace BoardSlide.API.Application.Cards.Commands.DeleteCard
{
    public class DeleteCardCommand : IRequest
    {
        public int BoardId { get; set; }
        public int CardListId { get; set; }
        public int CardId { get; set; }
    }
}