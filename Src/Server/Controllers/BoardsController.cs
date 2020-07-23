using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BoardSlide.API.Application.Boards.Commands.CreateBoard;
using BoardSlide.API.Application.Boards.Commands.DeleteBoard;
using BoardSlide.API.Application.Boards.Commands.UpdateBoard;
using BoardSlide.API.Application.Boards.Queries.GetBoard;
using BoardSlide.API.Application.Boards.Queries.GetBoards;
using BoardSlide.API.Server.Contracts;
using BoardSlide.API.Server.Contracts.Dtos.Boards;

namespace BoardSlide.API.Server.Controllers
{
    [Authorize]
    public class BoardsController : ApiController
    {
        public BoardsController(IUriService uriService)
            :base(uriService)
        {
        }


        [HttpGet(ApiRoutes.Boards.GetAll)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetBoardsQuery());
            return Ok(result);
        }

        [HttpGet(ApiRoutes.Boards.GetById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] int boardId)
        {
            var result = await Mediator.Send(new GetBoardQuery()
            {
                BoardId = boardId
            });
            return Ok(result);
        }

        [HttpPost(ApiRoutes.Boards.Post)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] BoardForCreationDto dto)
        {
            var result = await Mediator.Send(new CreateBoardCommand()
            {
                Name = dto.Name
            });
            return Created(UriService.GetBoardUri(result.Id), result);
        }

        [HttpPut(ApiRoutes.Boards.Put)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromRoute] int boardId, [FromBody] BoardForUpdateDto dto)
        {
            var result = await Mediator.Send(new UpdateBoardCommand()
            {
                BoardId = boardId,
                Name = dto.Name
            });
            return Ok(result);
        }

        [HttpDelete(ApiRoutes.Boards.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int boardId)
        {
            await Mediator.Send(new DeleteBoardCommand()
            {
                BoardId = boardId
            });
            return NoContent();
        }
    }
}