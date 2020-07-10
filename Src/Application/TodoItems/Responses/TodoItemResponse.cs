using Domain.Entities;

namespace Application.TodoItems.Responses
{
    public class TodoItemResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}