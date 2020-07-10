using System.Threading;
using System.Threading.Tasks;
using BoardSlide.API.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BoardSlide.API.Application.TodoItems.Commands.CreateTodoItem
{
    public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateTodoItemCommandValidator(IApplicationDbContext context)
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

        public async Task<bool> HaveUniqueName(string name, CancellationToken cancellationToken)
        {
            return await _context.TodoItems.AllAsync(item => item.Name != name);
        }
    }
}