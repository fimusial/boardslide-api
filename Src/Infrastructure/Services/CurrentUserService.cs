using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using BoardSlide.API.Application.Common.Interfaces;

namespace BoardSlide.API.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public string UserId { get; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
            => UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}