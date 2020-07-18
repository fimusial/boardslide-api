using System;

namespace BoardSlide.API.Application.Cards.Responses
{
    public class CardResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public int CardListId { get; set; }
    }
}