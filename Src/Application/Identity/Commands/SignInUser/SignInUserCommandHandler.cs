using System.Threading;
using System.Threading.Tasks;
using BoardSlide.API.Application.Common.Exceptions;
using BoardSlide.API.Application.Common.Interfaces;
using BoardSlide.API.Application.Common.Models;
using BoardSlide.API.Application.Identity.Responses;
using MediatR;

namespace BoardSlide.API.Application.Identity.Commands.SignInUser
{
    public class SignInUserCommandHandler : IRequestHandler<SignInUserCommand, TokenResponse>
    {
        private readonly IIdentityService _identityService;

        public SignInUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<TokenResponse> Handle(SignInUserCommand request, CancellationToken cancellationToken)
        {
            AuthenticationResult result = await _identityService.SignInUserAsync(request.UserName, request.Password);
            if (!result.Success)
            {
                throw new UnauthorizedException(result.GetErrorMessage());
            }

            return new TokenResponse() { Token = result.Token };
        }
    }
}