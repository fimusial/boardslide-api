using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BoardSlide.API.Application.CardLists.Responses;
using BoardSlide.API.Application.Common;
using BoardSlide.API.Application.Common.Interfaces.Identity;
using BoardSlide.API.Application.Common.Interfaces.Repositories;
using MediatR;

namespace BoardSlide.API.Application.CardLists.Queries.GetCardLists
{
    public class GetCardListsQueryHandler : DbHandlerBase, IRequestHandler<GetCardListsQuery, IEnumerable<CardListInfoResponse>>
    {
        public GetCardListsQueryHandler(ICurrentUserService currentUser, IUnitOfWork unit, IMapper mapper)
            : base(currentUser, unit, mapper)
        {
        }

        public async Task<IEnumerable<CardListInfoResponse>> Handle(GetCardListsQuery request, CancellationToken cancellationToken)
        {
            EnsureBoardExistsAndBelongsToCurrentUser(await _unit.Boards.GetBoardAsync(request.BoardId));

            return _mapper.Map<IEnumerable<CardListInfoResponse>>(
                await _unit.CardLists.FindAsync(list => list.BoardId == request.BoardId));
        }
    }
}