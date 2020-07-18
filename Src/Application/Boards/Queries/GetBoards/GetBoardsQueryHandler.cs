using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BoardSlide.API.Application.Boards.Responses;
using BoardSlide.API.Application.Common;
using BoardSlide.API.Application.Common.Interfaces;
using BoardSlide.API.Application.Common.Interfaces.Repositories;
using MediatR;

namespace BoardSlide.API.Application.Boards.Queries.GetBoards
{
    public class GetBoardsQueryHandler : HandlerBase, IRequestHandler<GetBoardsQuery, IEnumerable<BoardInfoResponse>>
    {
        public GetBoardsQueryHandler(ICurrentUserService currentUser, IUnitOfWork unit, IMapper mapper)
            : base(currentUser, unit, mapper)
        {
        }

        public async Task<IEnumerable<BoardInfoResponse>> Handle(GetBoardsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<BoardInfoResponse>>(
                await _unit.Boards.FindAsync(board => board.OwnerId == _currentUser.UserId));
        }
    }
}