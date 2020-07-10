using MediatR;

namespace BoardSlide.API.Application.TodoItems.Commands.DeleteTodoItem
{
    public class DeleteTodoItemCommand : IRequest
    {
        public int Id { get; set; }
    }
}