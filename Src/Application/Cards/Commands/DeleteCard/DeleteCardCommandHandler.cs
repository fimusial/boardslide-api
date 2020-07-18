using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BoardSlide.API.Application.Common;
using BoardSlide.API.Application.Common.Exceptions;
using BoardSlide.API.Application.Common.Interfaces;
using BoardSlide.API.Application.Common.Interfaces.Repositories;
using BoardSlide.API.Domain.Entities;
using MediatR;

namespace BoardSlide.API.Application.Cards.Commands.DeleteCard
{
    public class DeleteCardCommandHandler : HandlerBase, IRequestHandler<DeleteCardCommand>
    {
        public DeleteCardCommandHandler(ICurrentUserService currentUser, IUnitOfWork unit, IMapper mapper)
            : base(currentUser, unit, mapper)
        {
        }

        public async Task<Unit> Handle(DeleteCardCommand request, CancellationToken cancellationToken)
        {
            EnsureBoardExistsAndBelongsToCurrentUser(await _unit.Boards.GetBoardAsync(request.BoardId));

            Card card = await _unit.Cards.GetCardWithCardListIdAndBoardIdAsync(
                request.CardId, request.CardListId, request.BoardId)
                ?? throw new NotFoundException("Resource does not exist.");

            _unit.Cards.Remove(card);
            await _unit.CompleteAsync();
            return Unit.Value;
        }
    }
}