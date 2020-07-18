using System.Collections.Generic;

namespace BoardSlide.API.Domain.Entities
{
    public class CardList
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int BoardId { get; set; }
        public Board Board { get; set; }
        public ICollection<Card> Cards { get; set; }

        public CardList()
        {
            Cards = new HashSet<Card>();
        }
    }
}