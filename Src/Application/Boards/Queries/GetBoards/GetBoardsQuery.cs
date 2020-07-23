using System.Collections.Generic;
using BoardSlide.API.Application.Boards.Responses;
using BoardSlide.API.Application.Common.Interfaces.Cache;
using MediatR;

namespace BoardSlide.API.Application.Boards.Queries.GetBoards
{
    [CachedRequest(300)]
    public class GetBoardsQuery : IRequest<IEnumerable<BoardInfoResponse>>
    {
    }
}