using System.Threading;
using System.Threading.Tasks;
using BoardSlide.API.Application.Common.Interfaces;
using BoardSlide.API.Application.TodoItems.Responses;
using AutoMapper;
using BoardSlide.API.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BoardSlide.API.Application.TodoItems.Commands.CreateTodoItem
{
    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, TodoItemResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateTodoItemCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TodoItemResponse> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            TodoItem item = _mapper.Map<TodoItem>(request);
            EntityEntry<TodoItem> entry = _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
            return _mapper.Map<TodoItemResponse>(entry.Entity);
        }
    }
}