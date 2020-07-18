using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BoardSlide.API.Application.Boards.Responses;
using BoardSlide.API.Application.Common;
using BoardSlide.API.Application.Common.Interfaces;
using BoardSlide.API.Application.Common.Interfaces.Repositories;
using BoardSlide.API.Domain.Entities;
using MediatR;

namespace BoardSlide.API.Application.Boards.Commands.CreateBoard
{
    public class CreateBoardCommandHandler : HandlerBase, IRequestHandler<CreateBoardCommand, BoardInfoResponse>
    {
        public CreateBoardCommandHandler(ICurrentUserService currentUser, IUnitOfWork unit, IMapper mapper)
            : base(currentUser, unit, mapper)
        {
        }

        public async Task<BoardInfoResponse> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
        {
            Board board = _mapper.Map<Board>(request);
            board.OwnerId = _currentUser.UserId;
            
            Board createdBoard = await _unit.Boards.AddAsync(board);
            await _unit.CompleteAsync();
            return _mapper.Map<BoardInfoResponse>(createdBoard);
        }
    }
}