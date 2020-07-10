using System.Threading.Tasks;
using BoardSlide.API.Application.TodoItems.Commands.CreateTodoItem;
using BoardSlide.API.Application.TodoItems.Commands.DeleteTodoItem;
using BoardSlide.API.Application.TodoItems.Commands.UpdateTodoItem;
using BoardSlide.API.Application.TodoItems.Queries.GetTodoItem;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BoardSlide.API.Server.Dtos.TodoItems;

namespace BoardSlide.API.Server.Controllers
{
    public class TodoItemsController : ApiController
    {
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return Ok(await Mediator.Send(new GetTodoItemQuery()
            {
                Id = id
            }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] TodoItemForCreationDto dto)
        {
            var result = await Mediator.Send(new CreateTodoItemCommand()
            {
                Name = dto.Name,
                Description = dto.Description
            });
            return CreatedAtAction(nameof(Post), result);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] TodoItemForUpdateDto dto)
        {
            return Ok(await Mediator.Send(new UpdateTodoItemCommand()
            {
                Id = id,
                Name = dto.Name,
                Description = dto.Description
            }));
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await Mediator.Send(new DeleteTodoItemCommand()
            {
                Id = id
            });
            return NoContent();
        }
    }
}