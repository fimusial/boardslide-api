using BoardSlide.API.Application.Identity.Responses;
using MediatR;

namespace BoardSlide.API.Application.Identity.Commands.SignInUser
{
    public class SignInUserCommand : IRequest<TokenResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}