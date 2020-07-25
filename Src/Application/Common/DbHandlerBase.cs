using AutoMapper;
using BoardSlide.API.Application.Common.Exceptions;
using BoardSlide.API.Application.Common.Interfaces.Identity;
using BoardSlide.API.Application.Common.Interfaces.Repositories;
using BoardSlide.API.Domain.Entities;

namespace BoardSlide.API.Application.Common
{
    public class DbHandlerBase
    {
        protected readonly ICurrentUserService _currentUser;
        protected readonly IUnitOfWork _unit;
        protected readonly IMapper _mapper;

        protected DbHandlerBase(ICurrentUserService currentUser, IUnitOfWork unit, IMapper mapper)
        {
            _currentUser = currentUser;
            _unit = unit;
            _mapper = mapper;
        }

        protected void EnsureBoardExistsAndBelongsToCurrentUser(Board board)
        {
            if (board == null)
            {
                throw new NotFoundException("Resource does not exist.");
            }
            
            if (board.OwnerId != _currentUser.UserId)
            {
                throw new UnauthorizedException($"User {_currentUser.UserId} is not the owner of board {board.Id}.");
            }
        }
    }
}