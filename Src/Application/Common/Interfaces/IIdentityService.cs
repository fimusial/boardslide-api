using System.Threading.Tasks;
using BoardSlide.API.Application.Common.Models;

namespace BoardSlide.API.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<Result> RegisterUserAsync(string userName, string password);
        Task<AuthenticationResult> SignInUserAsync(string userName, string password);
        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
    }
}