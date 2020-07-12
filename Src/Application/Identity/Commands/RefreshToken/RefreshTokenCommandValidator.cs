using FluentValidation;

namespace BoardSlide.API.Application.Identity.Commands.RefreshToken
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(request => request.Token)
                .NotEmpty().WithMessage("Token is required.");

            RuleFor(request => request.RefreshToken)
                .NotEmpty().WithMessage("RefreshToken is required.");
        }
    }
}