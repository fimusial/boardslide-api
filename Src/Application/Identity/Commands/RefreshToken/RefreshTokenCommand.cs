using BoardSlide.API.Application.Identity.Responses;
using MediatR;

namespace BoardSlide.API.Application.Identity.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<TokenResponse>
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}