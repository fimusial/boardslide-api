using BoardSlide.API.Application.CardLists.Responses;
using BoardSlide.API.Application.Common.Interfaces.Cache;
using MediatR;

namespace BoardSlide.API.Application.CardLists.Queries.GetCardList
{
    [CachedRequest(300)]
    public class GetCardListQuery : IRequest<CardListResponse>
    {
        public int BoardId { get; set; }
        public int CardListId { get; set; }
    }
}