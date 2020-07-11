using FluentValidation;

namespace BoardSlide.API.Application.Identity.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(request => request.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .Matches(@"^[a-zA-Z0-9_]*$").WithMessage("Username can only contain alphanumeric characters and underscores (_).");

            RuleFor(request => request.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long")
                .Matches(@"^.*[a-z].*$").WithMessage("Pasword must contain a lowercase character.")
                .Matches(@"^.*[A-Z].*$").WithMessage("Pasword must contain an uppercase character.")
                .Matches(@"^.*[0-9].*$").WithMessage("Pasword must contain a digit.");
        }
    }
}