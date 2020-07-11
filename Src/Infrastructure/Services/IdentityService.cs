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

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<Result> RegisterUserAsync(string userName, string password)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(userName);
            if (user != null)
            {
                return Result.Failure(new string[] { $"User {userName} already exists." });
            }

            var newUser = new ApplicationUser
            {
                UserName = userName,
                Id = Guid.NewGuid().ToString()
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
                return AuthenticationResult.Failure(new string[] { $"Incorrect username or password." });
            }

            return AuthenticationResult.Successful(GenerateJwtToken(user));
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id)
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

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}