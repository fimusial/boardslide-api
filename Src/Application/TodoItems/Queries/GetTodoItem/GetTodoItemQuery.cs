using BoardSlide.API.Application.TodoItems.Responses;
using MediatR;

namespace BoardSlide.API.Application.TodoItems.Queries.GetTodoItem
{
    public class GetTodoItemQuery : IRequest<TodoItemResponse>
    {
        public int Id { get; set; }
    }
}