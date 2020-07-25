using System.Threading;
using System.Threading.Tasks;
using BoardSlide.API.Application.Boards.Queries.GetBoard;
using BoardSlide.API.Application.Cards.Responses;
using BoardSlide.API.Application.Common;
using BoardSlide.API.Application.Common.Interfaces.Cache;
using MediatR.Pipeline;

namespace BoardSlide.API.Application.Cards.Commands.CreateCard
{
    public class CreateCardCommandPostProcessor : CacheInvalidationPostProcessorBase,
        IRequestPostProcessor<CreateCardCommand, CardResponse>
    {
        public CreateCardCommandPostProcessor(ICacheService cache, ICacheKeyGenerator keyGenerator)
            : base(cache, keyGenerator)
        {
        }

        public async Task Process(CreateCardCommand request, CardResponse response, CancellationToken cancellationToken)
        {
            await InvalidateCacheForRequest(new GetBoardQuery() { BoardId = request.BoardId });
        }
    }
}