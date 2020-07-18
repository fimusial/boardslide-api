using BoardSlide.API.Application.CardLists.Responses;
using MediatR;

namespace BoardSlide.API.Application.CardLists.Commands.UpdateCardList
{
    public class UpdateCardListCommand : IRequest<CardListInfoResponse>
    {
        public int BoardId { get; set; }
        public int CardListId { get; set; }
        public string Name { get; set; }
    }
}