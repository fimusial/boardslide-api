using FluentValidation;

namespace BoardSlide.API.Application.CardLists.Commands.UpdateCardList
{
    public class UpdateCardListCommandValidator : AbstractValidator<UpdateCardListCommand>
    {
        public UpdateCardListCommandValidator()
        {
            RuleFor(request => request.Name)
               .NotEmpty().WithMessage("Name is required.")
               .MaximumLength(32).WithMessage("Name must not exceed 32 characters.");
        }
    }
}