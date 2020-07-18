using System;
using BoardSlide.API.Application.Cards.Responses;
using MediatR;

namespace BoardSlide.API.Application.Cards.Commands.CreateCard
{
    public class CreateCardCommand : IRequest<CardResponse>
    {
        public int BoardId { get; set; }
        public int CardListId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
    }
}