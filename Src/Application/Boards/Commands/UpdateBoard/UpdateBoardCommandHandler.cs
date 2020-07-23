using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BoardSlide.API.Application.Boards.Responses;
using BoardSlide.API.Application.Common;
using BoardSlide.API.Application.Common.Interfaces.Identity;
using BoardSlide.API.Application.Common.Interfaces.Repositories;
using BoardSlide.API.Domain.Entities;
using MediatR;

namespace BoardSlide.API.Application.Boards.Commands.UpdateBoard
{
    public class UpdateBoardCommandHandler : HandlerBase, IRequestHandler<UpdateBoardCommand, BoardInfoResponse>
    {
        public UpdateBoardCommandHandler(ICurrentUserService currentUser, IUnitOfWork unit, IMapper mapper)
            : base(currentUser, unit, mapper)
        {
        }

        public async Task<BoardInfoResponse> Handle(UpdateBoardCommand request, CancellationToken cancellationToken)
        {
            Board board = await _unit.Boards.GetAsync(request.BoardId);
            EnsureBoardExistsAndBelongsToCurrentUser(board);

            _mapper.Map(request, board);
            Board updatedBoard = _unit.Boards.Update(board);
            await _unit.CompleteAsync();
            return _mapper.Map<BoardInfoResponse>(updatedBoard);
        }
    }
}