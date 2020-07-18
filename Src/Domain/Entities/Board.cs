using System.Collections.Generic;

namespace BoardSlide.API.Domain.Entities
{
    public class Board
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string OwnerId { get; set; }
        public ICollection<CardList> CardLists { get; set; }

        public Board()
        {
            CardLists = new HashSet<CardList>();
        }
    }
}