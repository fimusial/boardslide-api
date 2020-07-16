using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BoardSlide.API.Application.Common.Interfaces;
using BoardSlide.API.Application.Common.Models;
using BoardSlide.API.Infrastructure.Identity;
using BoardSlide.API.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BoardSlide.API.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IdentityDbContext _identityContext;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<JwtSettings> jwtSettings,
            TokenValidationParameters tokenValidationParameters,
            IdentityDbContext identityContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
            _tokenValidationParameters = tokenValidationParameters.Clone();
            _identityContext = identityContext;

            _tokenValidationParameters.ValidateLifetime = false;
        }

        public async Task<Result> RegisterUserAsync(string userName, string password)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                return Result.Failure($"User {userName} already exists.");
            }

            var newUser = new ApplicationUser
            {
                UserName = userName
            };

            IdentityResult identityResult = await _userManager.CreateAsync(newUser, password);
            if (!identityResult.Succeeded)
            {
                return Result.Failure(identityResult.Errors.Select(x => x.Description));
            }

            return Result.Successful();
        }

        public async Task<AuthenticationResult> SignInUserAsync(string userName, string password)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(userName);
            SignInResult signInResult = null;

            if (user != null)
            {
                signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            }

            if (signInResult == null || !signInResult.Succeeded)
            {
                return AuthenticationResult.Failure("Incorrect username or password.");
            }

            return await GenerateTokensAsync(user);
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshTokenBase64)
        {
            string refreshToken = Base64ToString(refreshTokenBase64);

            DateTime now = DateTime.UtcNow;
            ClaimsPrincipal principal = GetClaimsPrincipalFromToken(token);
            if (principal == null)
            {
                return AuthenticationResult.Failure("Invalid token.");
            }

            if (!HasTokenExpired(principal, now))
            {
                return AuthenticationResult.Failure("The token has not yet expired.");
            }

            RefreshTokenInfo storedRefreshTokenInfo = await _identityContext.RefreshTokens.FindAsync(refreshToken);

            if (storedRefreshTokenInfo == null)
            {
                return AuthenticationResult.Failure("The refresh token does not exist.");
            }

            if (storedRefreshTokenInfo.ExpirationDateTime < now)
            {
                return AuthenticationResult.Failure("The refresh token has expired.");
            }

            if (storedRefreshTokenInfo.WasInvalidated)
            {
                return AuthenticationResult.Failure("The refresh token has been invalidated.");
            }

            if (storedRefreshTokenInfo.WasUsed)
            {
                return AuthenticationResult.Failure("The refresh token has already been used.");
            }

            string jti = principal.FindFirstValue(JwtRegisteredClaimNames.Jti);
            if (storedRefreshTokenInfo.Jti != jti)
            {
                return AuthenticationResult.Failure("Token ids do not match.");
            }

            storedRefreshTokenInfo.WasUsed = true;
            _identityContext.RefreshTokens.Update(storedRefreshTokenInfo);
            await _identityContext.SaveChangesAsync();

            string userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser user = await _userManager.FindByIdAsync(userId);

            return await GenerateTokensAsync(user);
        }

        private async Task<AuthenticationResult> GenerateTokensAsync(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            byte[] key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            DateTime expiration = DateTime.UtcNow.AddSeconds(_jwtSettings.ExpirationDurationInSeconds);

            var token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: credentials);

            DateTime now = DateTime.UtcNow;
            var refreshTokenInfo = new RefreshTokenInfo()
            {
                Jti = token.Id,
                UserId = user.Id,
                CreationDateTime = now,
                ExpirationDateTime = now.AddDays(_jwtSettings.RefreshTokenExpirationDurationInDays)
            };

            _identityContext.RefreshTokens.Add(refreshTokenInfo);
            await _identityContext.SaveChangesAsync();

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            string refreshTokenString = StringToBase64(refreshTokenInfo.Token);
            return AuthenticationResult.Successful(tokenString, refreshTokenString);
        }

        private ClaimsPrincipal GetClaimsPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }

                return principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
        }

        private bool HasTokenExpired(ClaimsPrincipal principal, DateTime now)
        {
            string expClaimValue = principal.FindFirstValue(JwtRegisteredClaimNames.Exp);
            long expirationUnixTimestamp = long.Parse(expClaimValue);
            DateTime expirationDateTime = DateTimeOffset.FromUnixTimeSeconds(expirationUnixTimestamp).UtcDateTime;

            if (expirationDateTime > now)
            {
                return false;
            }

            return true;
        }

        private string StringToBase64(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(bytes);
        }

        private string Base64ToString(string input)
        {
            byte[] bytes = Convert.FromBase64String(input);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}