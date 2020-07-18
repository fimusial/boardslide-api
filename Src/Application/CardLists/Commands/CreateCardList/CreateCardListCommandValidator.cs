using FluentValidation;

namespace BoardSlide.API.Application.CardLists.Commands.CreateCardList
{
    public class CreateCardListCommandValidator : AbstractValidator<CreateCardListCommand>
    {
        public CreateCardListCommandValidator()
        {
            RuleFor(request => request.Name)
               .NotEmpty().WithMessage("Name is required.")
               .MaximumLength(32).WithMessage("Name must not exceed 32 characters.");
        }
    }
}