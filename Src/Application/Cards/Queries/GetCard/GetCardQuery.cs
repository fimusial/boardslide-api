using BoardSlide.API.Application.Cards.Responses;
using BoardSlide.API.Application.Common.Interfaces.Cache;
using MediatR;

namespace BoardSlide.API.Application.Cards.Queries.GetCard
{
    [CachedRequest(300)]
    public class GetCardQuery : IRequest<CardResponse>
    {
        public int BoardId { get; set; }
        public int CardListId { get; set; }
        public int CardId { get; set; }
    }
}