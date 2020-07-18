using BoardSlide.API.Application.Boards.Responses;
using MediatR;

namespace BoardSlide.API.Application.Boards.Queries.GetBoard
{
    public class GetBoardQuery : IRequest<BoardResponse>
    {
        public int BoardId { get; set; }
    }
}