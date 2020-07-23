using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BoardSlide.API.Application.CardLists.Responses;
using BoardSlide.API.Application.Common;
using BoardSlide.API.Application.Common.Exceptions;
using BoardSlide.API.Application.Common.Interfaces.Identity;
using BoardSlide.API.Application.Common.Interfaces.Repositories;
using MediatR;

namespace BoardSlide.API.Application.CardLists.Queries.GetCardList
{
    public class GetCardListQueryHandler : HandlerBase, IRequestHandler<GetCardListQuery, CardListResponse>
    {
        public GetCardListQueryHandler(ICurrentUserService currentUser, IUnitOfWork unit, IMapper mapper)
            : base(currentUser, unit, mapper)
        {
        }

        public async Task<CardListResponse> Handle(GetCardListQuery request, CancellationToken cancellationToken)
        {
            EnsureBoardExistsAndBelongsToCurrentUser(await _unit.Boards.GetBoardAsync(request.BoardId));

            return _mapper.Map<CardListResponse>(await _unit.CardLists.GetCardListWithBoardIdAsync(
                request.CardListId, request.BoardId, includeCards: true))
                ?? throw new NotFoundException("Resource does not exist.");
        }
    }
}