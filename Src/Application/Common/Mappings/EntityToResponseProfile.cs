using AutoMapper;
using BoardSlide.API.Application.Boards.Responses;
using BoardSlide.API.Application.CardLists.Responses;
using BoardSlide.API.Application.Cards.Responses;
using BoardSlide.API.Domain.Entities;

namespace BoardSlide.API.Application.Common.Mappings
{
    public class EntityToResponseProfile : Profile
    {
        public EntityToResponseProfile()
        {
            CreateMap<Board, BoardInfoResponse>();
            CreateMap<Board, BoardResponse>();
            CreateMap<CardList, CardListInfoResponse>();
            CreateMap<CardList, CardListResponse>();
            CreateMap<Card, CardResponse>();
        }
    }
}