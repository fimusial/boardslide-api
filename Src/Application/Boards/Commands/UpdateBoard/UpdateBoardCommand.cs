using BoardSlide.API.Application.Boards.Responses;
using MediatR;

namespace BoardSlide.API.Application.Boards.Commands.UpdateBoard
{
    public class UpdateBoardCommand : IRequest<BoardInfoResponse>
    {
        public int BoardId { get; set; }
        public string Name { get; set; }
    }
}