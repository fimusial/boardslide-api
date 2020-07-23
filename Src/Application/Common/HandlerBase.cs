using AutoMapper;
using BoardSlide.API.Application.Common.Exceptions;
using BoardSlide.API.Application.Common.Interfaces;
using BoardSlide.API.Application.Common.Interfaces.Repositories;
using BoardSlide.API.Domain.Entities;

namespace BoardSlide.API.Application.Common
{
    public class HandlerBase
    {
        protected readonly ICurrentUserService _currentUser;
        protected readonly IUnitOfWork _unit;
        protected readonly IMapper _mapper;

        protected HandlerBase(ICurrentUserService currentUser, IUnitOfWork unit, IMapper mapper)
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