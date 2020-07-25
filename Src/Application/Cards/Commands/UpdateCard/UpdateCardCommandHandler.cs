using System.Linq;
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

namespace BoardSlide.API.Application.Cards.Commands.UpdateCard
{
    public class UpdateCardCommandHandler : DbHandlerBase, IRequestHandler<UpdateCardCommand, CardResponse>
    {
        public UpdateCardCommandHandler(ICurrentUserService currentUser, IUnitOfWork unit, IMapper mapper)
            : base(currentUser, unit, mapper)
        {
        }

        public async Task<CardResponse> Handle(UpdateCardCommand request, CancellationToken cancellationToken)
        {
            EnsureBoardExistsAndBelongsToCurrentUser(await _unit.Boards.GetBoardAsync(request.BoardId));

            Card card = await _unit.Cards.GetCardWithCardListIdAndBoardIdAsync(
                request.CardId, request.CardListId, request.BoardId)
                ?? throw new NotFoundException("Resource does not exist.");

            if (request.NewCardListId.HasValue)
            {
                Board board = await _unit.Boards.GetBoardAsync(request.BoardId, includeCardLists: true);
                if (board.CardLists.SingleOrDefault(list => list.Id == request.NewCardListId) != null)
                {
                    card.CardListId = request.NewCardListId.Value;
                }
                else
                {
                    throw new BadRequestException($"Cannot move card {request.CardId} to list {request.NewCardListId}.");
                }
            }

            _mapper.Map(request, card);
            Card updatedCard = _unit.Cards.Update(card);
            await _unit.CompleteAsync();
            return _mapper.Map<CardResponse>(updatedCard);
        }
    }
}