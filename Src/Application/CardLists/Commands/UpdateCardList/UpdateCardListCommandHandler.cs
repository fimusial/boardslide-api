using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BoardSlide.API.Application.CardLists.Responses;
using BoardSlide.API.Application.Common;
using BoardSlide.API.Application.Common.Exceptions;
using BoardSlide.API.Application.Common.Interfaces.Identity;
using BoardSlide.API.Application.Common.Interfaces.Repositories;
using BoardSlide.API.Domain.Entities;
using MediatR;

namespace BoardSlide.API.Application.CardLists.Commands.UpdateCardList
{
    public class UpdateCardListCommandHandler : DbHandlerBase, IRequestHandler<UpdateCardListCommand, CardListInfoResponse>
    {
        public UpdateCardListCommandHandler(ICurrentUserService currentUser, IUnitOfWork unit, IMapper mapper)
            : base(currentUser, unit, mapper)
        {
        }

        public async Task<CardListInfoResponse> Handle(UpdateCardListCommand request, CancellationToken cancellationToken)
        {
            EnsureBoardExistsAndBelongsToCurrentUser(await _unit.Boards.GetBoardAsync(request.BoardId));

            CardList cardList = await _unit.CardLists.GetCardListWithBoardIdAsync(
                request.CardListId, request.BoardId) ?? throw new NotFoundException("Resource does not exist.");

            _mapper.Map(request, cardList);
            CardList updatedCardList = _unit.CardLists.Update(cardList);
            await _unit.CompleteAsync();
            return _mapper.Map<CardListInfoResponse>(updatedCardList);
        }
    }
}