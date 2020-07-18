using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BoardSlide.API.Application.Boards.Responses;
using BoardSlide.API.Application.Common;
using BoardSlide.API.Application.Common.Interfaces;
using BoardSlide.API.Application.Common.Interfaces.Repositories;
using BoardSlide.API.Domain.Entities;
using MediatR;

namespace BoardSlide.API.Application.Boards.Queries.GetBoard
{
    public class GetBoardQueryHandler : HandlerBase, IRequestHandler<GetBoardQuery, BoardResponse>
    {
        public GetBoardQueryHandler(ICurrentUserService currentUser, IUnitOfWork unit, IMapper mapper)
            : base(currentUser, unit, mapper)
        {
        }

        public async Task<BoardResponse> Handle(GetBoardQuery request, CancellationToken cancellationToken)
        {
            EnsureBoardExistsAndBelongsToCurrentUser(await _unit.Boards.GetAsync(request.BoardId));

            return _mapper.Map<BoardResponse>(
                await _unit.Boards.GetBoardAsync(request.BoardId, includeCardLists: true, includeCards: true));
        }
    }
}