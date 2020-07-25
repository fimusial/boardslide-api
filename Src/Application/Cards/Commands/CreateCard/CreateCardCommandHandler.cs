using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using AutoMapper;
using BoardSlide.API.Application.Cards.Responses;
using BoardSlide.API.Application.Common;
using BoardSlide.API.Application.Common.Exceptions;
using BoardSlide.API.Application.Common.Interfaces.Identity;
using BoardSlide.API.Application.Common.Interfaces.Repositories;
using BoardSlide.API.Application.Common.Settings;
using BoardSlide.API.Domain.Entities;
using MediatR;

namespace BoardSlide.API.Application.Cards.Commands.CreateCard
{
    public class CreateCardCommandHandler : DbHandlerBase, IRequestHandler<CreateCardCommand, CardResponse>
    {
        private readonly ApplicationSettings _settings;

        public CreateCardCommandHandler(ICurrentUserService currentUser, IUnitOfWork unit, IMapper mapper,
            IOptions<ApplicationSettings> settings)
            : base(currentUser, unit, mapper)
        {
            _settings = settings.Value;
        }

        public async Task<CardResponse> Handle(CreateCardCommand request, CancellationToken cancellationToken)
        {
            EnsureBoardExistsAndBelongsToCurrentUser(await _unit.Boards.GetBoardAsync(request.BoardId));

            CardList cardList = await _unit.CardLists.GetCardListWithBoardIdAsync(request.CardListId,
                request.BoardId, includeCards: true)
                ?? throw new NotFoundException("Resource does not exist.");

            if (cardList.Cards.Count >= _settings.CardsPerCardListLimit)
            {
                throw new BadRequestException($"Cards limit for list {request.CardListId} exceeded.");
            }

            Card card = _mapper.Map<Card>(request);
            card.CardListId = request.CardListId;

            Card createdCard = await _unit.Cards.AddAsync(card);
            await _unit.CompleteAsync();
            return _mapper.Map<CardResponse>(createdCard);
        }
    }
}