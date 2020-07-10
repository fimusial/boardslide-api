using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BoardSlide.API.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BoardSlide.API.Application.TodoItems.Commands.UpdateTodoItem
{
    public class UpdateTodoItemCommandValidator : AbstractValidator<UpdateTodoItemCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTodoItemCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(request => request.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(255).WithMessage("Name must not exceed 200 characters.")
                .MustAsync(HaveUniqueName).WithMessage("An item with the specified name already exists.");

            RuleFor(request => request.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(255).WithMessage("Description must not exceed 200 characters.");
        }

        public async Task<bool> HaveUniqueName(UpdateTodoItemCommand request, string name, CancellationToken cancellationToken)
        {
            return await _context.TodoItems
                .Where(item => item.Id != request.Id)
                .AllAsync(item => item.Name != name);
        }
    }
}