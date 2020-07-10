using BoardSlide.API.Application.TodoItems.Responses;
using AutoMapper;
using BoardSlide.API.Domain.Entities;

namespace BoardSlide.API.Application.Common.Mappings
{
    public class EntityToResponseProfile : Profile
    {
        public EntityToResponseProfile()
        {
            CreateMap<TodoItem, TodoItemResponse>();
        }
    }
}