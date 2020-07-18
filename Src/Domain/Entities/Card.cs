using System;

namespace BoardSlide.API.Domain.Entities
{
    public class Card
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }

        public int CardListId { get; set; }
        public CardList CardList { get; set; }
    }
}