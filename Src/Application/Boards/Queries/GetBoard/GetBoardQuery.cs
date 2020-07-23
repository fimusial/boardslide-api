using BoardSlide.API.Application.Boards.Responses;
using BoardSlide.API.Application.Common.Interfaces.Cache;
using MediatR;

namespace BoardSlide.API.Application.Boards.Queries.GetBoard
{
    [CachedRequest(300)]
    public class GetBoardQuery : IRequest<BoardResponse>
    {
        public int BoardId { get; set; }
    }
}