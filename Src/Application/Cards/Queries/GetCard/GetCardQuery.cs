using BoardSlide.API.Application.Cards.Responses;
using MediatR;

namespace BoardSlide.API.Application.Cards.Queries.GetCard
{
    public class GetCardQuery : IRequest<CardResponse>
    {
        public int BoardId { get; set; }
        public int CardListId { get; set; }
        public int CardId { get; set; }
    }
}