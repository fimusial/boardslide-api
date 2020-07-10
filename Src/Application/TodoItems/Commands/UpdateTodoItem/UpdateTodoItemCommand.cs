using BoardSlide.API.Application.TodoItems.Responses;
using MediatR;

namespace BoardSlide.API.Application.TodoItems.Commands.UpdateTodoItem
{
    public class UpdateTodoItemCommand : IRequest<TodoItemResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}