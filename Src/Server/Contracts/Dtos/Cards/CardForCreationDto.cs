using System;

namespace BoardSlide.API.Server.Contracts.Dtos.Cards
{
    public class CardForCreationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
    }
}