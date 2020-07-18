using BoardSlide.API.Application.CardLists.Responses;
using MediatR;

namespace BoardSlide.API.Application.CardLists.Commands.CreateCardList
{
    public class CreateCardListCommand : IRequest<CardListInfoResponse>
    {
        public int BoardId { get; set; }
        public string Name { get; set; }
    }
}