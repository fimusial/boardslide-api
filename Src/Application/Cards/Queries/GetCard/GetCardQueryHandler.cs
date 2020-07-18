using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BoardSlide.API.Application.Cards.Responses;
using BoardSlide.API.Application.Common;
using BoardSlide.API.Application.Common.Exceptions;
using BoardSlide.API.Application.Common.Interfaces;
using BoardSlide.API.Application.Common.Interfaces.Repositories;
using MediatR;

namespace BoardSlide.API.Application.Cards.Queries.GetCard
{
    public class GetCardQueryHandler : HandlerBase, IRequestHandler<GetCardQuery, CardResponse>
    {
        public GetCardQueryHandler(ICurrentUserService currentUser, IUnitOfWork unit, IMapper mapper)
            : base(currentUser, unit, mapper)
        {
        }

        public async Task<CardResponse> Handle(GetCardQuery request, CancellationToken cancellationToken)
        {
            EnsureBoardExistsAndBelongsToCurrentUser(await _unit.Boards.GetBoardAsync(request.BoardId));

            return _mapper.Map<CardResponse>(await _unit.Cards.GetCardWithCardListIdAndBoardIdAsync(
                request.CardId, request.CardListId, request.BoardId))
                ?? throw new NotFoundException("Resource does not exist.");
        }
    }
}