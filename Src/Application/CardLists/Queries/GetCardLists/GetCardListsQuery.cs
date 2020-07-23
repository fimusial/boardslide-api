using System.Collections.Generic;
using BoardSlide.API.Application.CardLists.Responses;
using BoardSlide.API.Application.Common.Interfaces.Cache;
using MediatR;

namespace BoardSlide.API.Application.CardLists.Queries.GetCardLists
{
    [CachedRequest(300)]
    public class GetCardListsQuery : IRequest<IEnumerable<CardListInfoResponse>>
    {
        public int BoardId { get; set; }
    }
}