using System.Collections.Generic;
using BoardSlide.API.Application.Cards.Responses;
using BoardSlide.API.Application.Common.Interfaces.Cache;
using MediatR;

namespace BoardSlide.API.Application.Cards.Queries.GetCards
{
    [CachedRequest(300)]
    public class GetCardsQuery :  IRequest<IEnumerable<CardResponse>>
    {
        public int BoardId { get; set; }
        public int CardListId { get; set; }
    }
}