using MediatR;

namespace BoardSlide.API.Application.Identity.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}