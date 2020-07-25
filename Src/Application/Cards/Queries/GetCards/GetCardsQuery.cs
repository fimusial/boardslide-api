using System.Collections.Generic;
using BoardSlide.API.Application.Cards.Responses;
using MediatR;

namespace BoardSlide.API.Application.Cards.Queries.GetCards
{
    public class GetCardsQuery :  IRequest<IEnumerable<CardResponse>>
    {
        public int BoardId { get; set; }
        public int CardListId { get; set; }
    }
}