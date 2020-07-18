using System.Collections.Generic;
using BoardSlide.API.Application.Cards.Responses;

namespace BoardSlide.API.Application.CardLists.Responses
{
    public class CardListResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BoardId { get; set; }
        public IEnumerable<CardResponse> Cards { get; set; }
    }
}