using System.Threading;
using System.Threading.Tasks;
using BoardSlide.API.Application.Common.Exceptions;
using BoardSlide.API.Application.Common.Interfaces.Identity;
using BoardSlide.API.Application.Common.Models;
using MediatR;

namespace BoardSlide.API.Application.Identity.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly IIdentityService _identityService;

        public RegisterUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            Result result = await _identityService.RegisterUserAsync(request.UserName, request.Password);
            if (!result.Success)
            {
                throw new BadRequestException(result.GetErrorMessage());
            }

            return Unit.Value;
        }
    }
}