using BoardSlide.API.Application.CardLists.Responses;
using MediatR;

namespace BoardSlide.API.Application.CardLists.Queries.GetCardList
{
    public class GetCardListQuery : IRequest<CardListResponse>
    {
        public int BoardId { get; set; }
        public int CardListId { get; set; }
    }
}