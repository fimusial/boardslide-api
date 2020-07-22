using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BoardSlide.API.Application.CardLists.Commands.CreateCardList;
using BoardSlide.API.Application.CardLists.Commands.DeleteCardList;
using BoardSlide.API.Application.CardLists.Commands.UpdateCardList;
using BoardSlide.API.Application.CardLists.Queries.GetCardList;
using BoardSlide.API.Application.CardLists.Queries.GetCardLists;
using BoardSlide.API.Server.Contracts;
using BoardSlide.API.Server.Contracts.Dtos.CardLists;

namespace BoardSlide.API.Server.Controllers
{
    [Authorize]
    public class CardListsController : ApiController
    {
        [HttpGet(ApiRoutes.CardLists.GetAll)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll([FromRoute] int boardId)
        {
            var result = await Mediator.Send(new GetCardListsQuery()
            {
                BoardId = boardId
            });
            return Ok(result);
        }

        [HttpGet(ApiRoutes.CardLists.GetById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] int boardId, [FromRoute] int cardListId)
        {
            var result = await Mediator.Send(new GetCardListQuery()
            {
                BoardId = boardId,
                CardListId = cardListId
            });
            return Ok(result);
        }

        [HttpPost(ApiRoutes.CardLists.Post)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromRoute] int boardId, [FromBody] CardListForCreationDto dto)
        {
            var result = await Mediator.Send(new CreateCardListCommand()
            {
                BoardId = boardId,
                Name = dto.Name
            });
            return CreatedAtAction(nameof(Post), result);
        }

        [HttpPut(ApiRoutes.CardLists.Put)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromRoute] int boardId, [FromRoute] int cardListId, [FromBody] CardListForUpdateDto dto)
        {
            var result = await Mediator.Send(new UpdateCardListCommand()
            {
                BoardId = boardId,
                CardListId = cardListId,
                Name = dto.Name
            });
            return Ok(result);
        }

        [HttpDelete(ApiRoutes.CardLists.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int boardId, [FromRoute] int cardListId)
        {
            await Mediator.Send(new DeleteCardListCommand()
            {
                BoardId = boardId,
                CardListId = cardListId
            });
            return NoContent();
        }
    }
}