using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using AutoMapper;
using BoardSlide.API.Application.CardLists.Responses;
using BoardSlide.API.Application.Common;
using BoardSlide.API.Application.Common.Exceptions;
using BoardSlide.API.Application.Common.Interfaces;
using BoardSlide.API.Application.Common.Interfaces.Repositories;
using BoardSlide.API.Application.Common.Settings;
using BoardSlide.API.Domain.Entities;
using MediatR;

namespace BoardSlide.API.Application.CardLists.Commands.CreateCardList
{
    public class CreateCardListCommandHandler : HandlerBase, IRequestHandler<CreateCardListCommand, CardListInfoResponse>
    {
        private readonly ApplicationSettings _settings;

        public CreateCardListCommandHandler(ICurrentUserService currentUser, IUnitOfWork unit, IMapper mapper,
            IOptions<ApplicationSettings> settings)
            : base(currentUser, unit, mapper)
        {
            _settings = settings.Value;
        }

        public async Task<CardListInfoResponse> Handle(CreateCardListCommand request, CancellationToken cancellationToken)
        {
            Board board = await _unit.Boards.GetBoardAsync(request.BoardId, includeCardLists: true);
            EnsureBoardExistsAndBelongsToCurrentUser(board);

            if (board.CardLists.Count >= _settings.CardListsPerBoardLimit)
            {
                throw new BadRequestException($"Card lists limit for board {request.BoardId} exceeded.");
            }

            CardList cardList = _mapper.Map<CardList>(request);
            cardList.BoardId = request.BoardId;

           CardList createdCardList = await _unit.CardLists.AddAsync(cardList);
            await _unit.CompleteAsync();
            return _mapper.Map<CardListInfoResponse>(createdCardList);
        }
    }
}