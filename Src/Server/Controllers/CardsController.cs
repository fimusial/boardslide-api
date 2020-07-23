using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BoardSlide.API.Application.Cards.Commands.CreateCard;
using BoardSlide.API.Application.Cards.Commands.DeleteCard;
using BoardSlide.API.Application.Cards.Commands.UpdateCard;
using BoardSlide.API.Application.Cards.Queries.GetCard;
using BoardSlide.API.Application.Cards.Queries.GetCards;
using BoardSlide.API.Server.Contracts;
using BoardSlide.API.Server.Contracts.Dtos.Cards;

namespace BoardSlide.API.Server.Controllers
{
    [Authorize]
    public class CardsController : ApiController
    {
        public CardsController(IUriService uriService)
            :base(uriService)
        {
        }

        [HttpGet(ApiRoutes.Cards.GetAll)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll([FromRoute] int boardId, [FromRoute] int cardListId)
        {
            var result = await Mediator.Send(new GetCardsQuery()
            {
                BoardId = boardId,
                CardListId = cardListId
            });
            return Ok(result);
        }

        [HttpGet(ApiRoutes.Cards.GetById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] int boardId, [FromRoute] int cardListId, [FromRoute] int cardId)
        {
            var result = await Mediator.Send(new GetCardQuery()
            {
                BoardId = boardId,
                CardListId = cardListId,
                CardId = cardId
            });
            return Ok(result);
        }

        [HttpPost(ApiRoutes.Cards.Post)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromRoute] int boardId, [FromRoute] int cardListId, [FromBody] CardForCreationDto dto)
        {
            var result = await Mediator.Send(new CreateCardCommand()
            {
                BoardId = boardId,
                CardListId = cardListId,
                Name = dto.Name,
                Description = dto.Description,
                DueDate = dto.DueDate
            });
            return Created(UriService.GetCardUri(boardId, cardListId, result.Id), result);
        }

        [HttpPut(ApiRoutes.Cards.Put)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromRoute] int boardId, [FromRoute] int cardListId, [FromRoute] int cardId, [FromBody] CardForUpdateDto dto)
        {
            var result = await Mediator.Send(new UpdateCardCommand()
            {
                BoardId = boardId,
                CardListId = cardListId,
                NewCardListId = dto.NewCardListId,
                CardId = cardId,
                Name = dto.Name,
                Description = dto.Description,
                DueDate = dto.DueDate
            });
            return Ok(result);
        }

        [HttpDelete(ApiRoutes.Cards.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int boardId, [FromRoute] int cardListId, [FromRoute] int cardId)
        {
            await Mediator.Send(new DeleteCardCommand()
            {
                BoardId = boardId,
                CardListId = cardListId,
                CardId = cardId
            });
            return NoContent();
        }
    }
}