using System;
using BoardSlide.API.Application.Cards.Responses;
using MediatR;

namespace BoardSlide.API.Application.Cards.Commands.UpdateCard
{
    public class UpdateCardCommand : IRequest<CardResponse>
    {
        public int BoardId { get; set; }
        public int CardListId { get; set; }
        public int? NewCardListId { get; set; }
        public int CardId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
    }
}