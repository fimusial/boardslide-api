using MediatR;

namespace BoardSlide.API.Application.Boards.Commands.DeleteBoard
{
    public class DeleteBoardCommand : IRequest
    {
        public int BoardId { get; set; }
    }
}