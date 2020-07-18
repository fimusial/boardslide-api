using System;

namespace BoardSlide.API.Server.Contracts.Dtos.Cards
{
    public class CardForUpdateDto
    {
        public int? NewCardListId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
    }
}