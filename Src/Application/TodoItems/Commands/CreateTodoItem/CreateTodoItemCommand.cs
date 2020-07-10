using BoardSlide.API.Application.TodoItems.Responses;
using MediatR;

namespace BoardSlide.API.Application.TodoItems.Commands.CreateTodoItem
{
    public class CreateTodoItemCommand : IRequest<TodoItemResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}