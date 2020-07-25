using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BoardSlide.API.Application.Common;
using BoardSlide.API.Application.Common.Interfaces.Identity;
using BoardSlide.API.Application.Common.Interfaces.Repositories;
using BoardSlide.API.Domain.Entities;
using MediatR;

namespace BoardSlide.API.Application.Boards.Commands.DeleteBoard
{
    public class DeleteBoardCommandHandler : DbHandlerBase, IRequestHandler<DeleteBoardCommand>
    {
        public DeleteBoardCommandHandler(ICurrentUserService currentUser, IUnitOfWork unit, IMapper mapper)
            : base(currentUser, unit, mapper)
        {
        }

        public async Task<Unit> Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
        {
            Board board = await _unit.Boards.GetAsync(request.BoardId);
            EnsureBoardExistsAndBelongsToCurrentUser(board);

            _unit.Boards.Remove(board);
            await _unit.CompleteAsync();
            return Unit.Value;
        }
    }
}