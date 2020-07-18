using System.Collections.Generic;
using BoardSlide.API.Application.Boards.Responses;
using MediatR;

namespace BoardSlide.API.Application.Boards.Queries.GetBoards
{
    public class GetBoardsQuery : IRequest<IEnumerable<BoardInfoResponse>>
    {
    }
}