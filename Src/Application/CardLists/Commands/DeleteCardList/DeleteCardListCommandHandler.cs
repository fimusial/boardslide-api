using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BoardSlide.API.Application.Common;
using BoardSlide.API.Application.Common.Exceptions;
using BoardSlide.API.Application.Common.Interfaces;
using BoardSlide.API.Application.Common.Interfaces.Repositories;
using BoardSlide.API.Domain.Entities;
using MediatR;

namespace BoardSlide.API.Application.CardLists.Commands.DeleteCardList
{
    public class DeleteCardListCommandHandler : HandlerBase, IRequestHandler<DeleteCardListCommand>
    {
        public DeleteCardListCommandHandler(ICurrentUserService currentUser, IUnitOfWork unit, IMapper mapper)
            : base(currentUser, unit, mapper)
        {
        }

        public async Task<Unit> Handle(DeleteCardListCommand request, CancellationToken cancellationToken)
        {
            EnsureBoardExistsAndBelongsToCurrentUser(await _unit.Boards.GetBoardAsync(request.BoardId));

            CardList cardList = await _unit.CardLists.GetCardListWithBoardIdAsync(
                request.CardListId, request.BoardId) ?? throw new NotFoundException("Resource does not exist.");

            _unit.CardLists.Remove(cardList);
            await _unit.CompleteAsync();
            return Unit.Value;
        }
    }
}