using BoardSlide.API.Application.TodoItems.Commands.CreateTodoItem;
using BoardSlide.API.Application.TodoItems.Commands.UpdateTodoItem;
using AutoMapper;
using BoardSlide.API.Domain.Entities;

namespace BoardSlide.API.Application.Common.Mappings
{
    public class RequestToEntityProfile : Profile
    {
        public RequestToEntityProfile()
        {
            CreateMap<CreateTodoItemCommand, TodoItem>();
            CreateMap<UpdateTodoItemCommand, TodoItem>()
                .ForMember(item => item.Id, options => options.Ignore());
        }
    }
}