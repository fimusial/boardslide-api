using System.Threading;
using System.Threading.Tasks;
using BoardSlide.API.Application.Common.Exceptions;
using BoardSlide.API.Application.Common.Interfaces;
using BoardSlide.API.Application.TodoItems.Responses;
using AutoMapper;
using BoardSlide.API.Domain.Entities;
using MediatR;

namespace BoardSlide.API.Application.TodoItems.Commands.UpdateTodoItem
{
    public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand, TodoItemResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateTodoItemCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TodoItemResponse> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
        {
            TodoItem dbItem = await _context.TodoItems.FindAsync(request.Id);
            if (dbItem == null)
            {
                throw new NotFoundException($"Todo item (id: {request.Id}) does not exist.");
            }
            
            _mapper.Map(request, dbItem);
            _context.TodoItems.Update(dbItem);
            await _context.SaveChangesAsync();
            return _mapper.Map<TodoItemResponse>(dbItem);
        }
    }
}