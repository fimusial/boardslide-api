using System.Collections.Generic;
using BoardSlide.API.Application.CardLists.Responses;

namespace BoardSlide.API.Application.Boards.Responses
{
    public class BoardResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<CardListResponse> CardLists { get; set; }
    }
}