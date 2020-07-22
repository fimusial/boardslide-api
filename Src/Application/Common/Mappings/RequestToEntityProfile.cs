using AutoMapper;
using BoardSlide.API.Application.Boards.Commands.CreateBoard;
using BoardSlide.API.Application.Boards.Commands.UpdateBoard;
using BoardSlide.API.Application.CardLists.Commands.CreateCardList;
using BoardSlide.API.Application.CardLists.Commands.UpdateCardList;
using BoardSlide.API.Application.Cards.Commands.CreateCard;
using BoardSlide.API.Application.Cards.Commands.UpdateCard;
using BoardSlide.API.Domain.Entities;

namespace BoardSlide.API.Application.Common.Mappings
{
    public class RequestToEntityProfile : Profile
    {
        public RequestToEntityProfile()
        {
            CreateMap<CreateBoardCommand, Board>()
                .ForMember(board => board.Id, options => options.Ignore())
                .ForMember(board => board.OwnerId, options => options.Ignore());
            CreateMap<UpdateBoardCommand, Board>()
                .ForMember(board => board.Id, options => options.Ignore())
                .ForMember(board => board.OwnerId, options => options.Ignore());

            CreateMap<CreateCardListCommand, CardList>()
                .ForMember(list => list.Id, options => options.Ignore())
                .ForMember(list => list.BoardId, options => options.Ignore());
            CreateMap<UpdateCardListCommand, CardList>()
                .ForMember(list => list.Id, options => options.Ignore())
                .ForMember(list => list.BoardId, options => options.Ignore());

            CreateMap<CreateCardCommand, Card>()
                .ForMember(card => card.Id, options => options.Ignore())
                .ForMember(card => card.CardListId, options => options.Ignore());
            CreateMap<UpdateCardCommand, Card>()
                .ForMember(card => card.Id, options => options.Ignore())
                .ForMember(card => card.CardListId, options => options.Ignore())
                .ForAllMembers(options => options.Condition((source, dest, member) => member != null));
        }
    }
}