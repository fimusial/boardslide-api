using FluentValidation;

namespace BoardSlide.API.Application.Cards.Commands.CreateCard
{
    public class CreateCardCommandValidator : AbstractValidator<CreateCardCommand>
    {
        public CreateCardCommandValidator()
        {
            RuleFor(request => request.Name)
               .NotEmpty().WithMessage("Name is required.")
               .MaximumLength(32).WithMessage("Name must not exceed 32 characters.");
        
            RuleFor(request => request.Description)
               .MaximumLength(255).WithMessage("Description must not exceed 255 characters.");
        }
    }
}