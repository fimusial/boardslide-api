using System.Collections.Generic;
using BoardSlide.API.Application.CardLists.Responses;
using MediatR;

namespace BoardSlide.API.Application.CardLists.Queries.GetCardLists
{
    public class GetCardListsQuery : IRequest<IEnumerable<CardListInfoResponse>>
    {
        public int BoardId { get; set; }
    }
}