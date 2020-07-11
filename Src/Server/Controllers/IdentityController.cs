using System.Threading.Tasks;
using BoardSlide.API.Application.Identity.Commands.RegisterUser;
using BoardSlide.API.Application.Identity.Commands.SignInUser;
using BoardSlide.API.Server.Dtos.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoardSlide.API.Server.Controllers
{
    [Route("api")]
    public class IdentityController : ApiController
    {
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto dto)
        {
            var result = await Mediator.Send(new RegisterUserCommand()
            {
                UserName = dto.UserName,
                Password = dto.Password
            });
            
            return NoContent();
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignInUser([FromBody] SignInDto dto)
        {
            var result = await Mediator.Send(new SignInUserCommand()
            {
                UserName = dto.UserName,
                Password = dto.Password
            });
            
            return Ok(result);
        }
    }
}