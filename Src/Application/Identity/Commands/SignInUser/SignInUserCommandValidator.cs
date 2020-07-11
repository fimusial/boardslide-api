using FluentValidation;

namespace BoardSlide.API.Application.Identity.Commands.SignInUser
{
    public class SignInUserCommandValidator : AbstractValidator<SignInUserCommand>
    {
        public SignInUserCommandValidator()
        {
            RuleFor(request => request.UserName)
                .NotEmpty().WithMessage("Username is required.");

            RuleFor(request => request.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}