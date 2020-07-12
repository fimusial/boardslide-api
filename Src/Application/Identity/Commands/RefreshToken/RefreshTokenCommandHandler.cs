using System.Threading;
using System.Threading.Tasks;
using BoardSlide.API.Application.Common.Exceptions;
using BoardSlide.API.Application.Common.Interfaces;
using BoardSlide.API.Application.Common.Models;
using BoardSlide.API.Application.Identity.Responses;
using MediatR;

namespace BoardSlide.API.Application.Identity.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenResponse>
    {
        private readonly IIdentityService _identityService;

        public RefreshTokenCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<TokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            AuthenticationResult result = await _identityService.RefreshTokenAsync(request.Token, request.RefreshToken);
            if (!result.Success)
            {
                throw new BadRequestException(result.GetErrorMessage());
            }

            return new TokenResponse()
            {
                Token = result.Token,
                RefreshToken = result.RefreshToken
            };
        }
    }
}