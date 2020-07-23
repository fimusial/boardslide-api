using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BoardSlide.API.Application.Cards.Responses;
using BoardSlide.API.Application.Common;
using BoardSlide.API.Application.Common.Exceptions;
using BoardSlide.API.Application.Common.Interfaces.Identity;
using BoardSlide.API.Application.Common.Interfaces.Repositories;
using BoardSlide.API.Domain.Entities;
using MediatR;

namespace BoardSlide.API.Application.Cards.Queries.GetCards
{
    public class GetCardsQueryHandler : HandlerBase, IRequestHandler<GetCardsQuery, IEnumerable<CardResponse>>
    {
        public GetCardsQueryHandler(ICurrentUserService currentUser, IUnitOfWork unit, IMapper mapper)
            : base(currentUser, unit, mapper)
        {
        }

        public async Task<IEnumerable<CardResponse>> Handle(GetCardsQuery request, CancellationToken cancellationToken)
        {
            EnsureBoardExistsAndBelongsToCurrentUser(await _unit.Boards.GetBoardAsync(request.BoardId));

            CardList cardList = await _unit.CardLists.GetCardListWithBoardIdAsync(
                request.CardListId, request.BoardId, includeCards: true);

            if (cardList == null)
            {
                throw new NotFoundException("Resource does not exist.");
            }

            return _mapper.Map<IEnumerable<CardResponse>>(cardList.Cards);
        }
    }
}