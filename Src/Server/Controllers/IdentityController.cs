using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BoardSlide.API.Application.Identity.Commands.RefreshToken;
using BoardSlide.API.Application.Identity.Commands.RegisterUser;
using BoardSlide.API.Application.Identity.Commands.SignInUser;
using BoardSlide.API.Server.Contracts;
using BoardSlide.API.Server.Contracts.Dtos.Identity;

namespace BoardSlide.API.Server.Controllers
{
    public class IdentityController : ApiController
    {
        public IdentityController(IUriService uriService)
            :base(uriService)
        {
        }

        [HttpPost(ApiRoutes.Identity.Register)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var result = await Mediator.Send(new RegisterUserCommand()
            {
                UserName = dto.UserName,
                Password = dto.Password
            });
            return NoContent();
        }

        [HttpPost(ApiRoutes.Identity.SignIn)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignIn([FromBody] SignInDto dto)
        {
            var result = await Mediator.Send(new SignInUserCommand()
            {
                UserName = dto.UserName,
                Password = dto.Password
            });
            return Ok(result);
        }

        [HttpPost(ApiRoutes.Identity.Refresh)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenDto dto)
        {
            var result = await Mediator.Send(new RefreshTokenCommand()
            {
                Token = dto.Token,
                RefreshToken = dto.RefreshToken
            });
            return Ok(result);
        }
    }
}