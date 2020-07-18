using System.Security.Claims;
using BoardSlide.API.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace BoardSlide.API.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public string UserId { get; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
            => UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}