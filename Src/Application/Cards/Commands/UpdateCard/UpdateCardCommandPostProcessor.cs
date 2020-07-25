using System.Threading;
using System.Threading.Tasks;
using BoardSlide.API.Application.Boards.Queries.GetBoard;
using BoardSlide.API.Application.Cards.Responses;
using BoardSlide.API.Application.Common;
using BoardSlide.API.Application.Common.Interfaces.Cache;
using MediatR.Pipeline;

namespace BoardSlide.API.Application.Cards.Commands.UpdateCard
{
    public class UpdateCardCommandPostProcessor : CacheInvalidationPostProcessorBase,
        IRequestPostProcessor<UpdateCardCommand, CardResponse>
    {
        public UpdateCardCommandPostProcessor(ICacheService cache, ICacheKeyGenerator keyGenerator)
            : base(cache, keyGenerator)
        {
        }

        public async Task Process(UpdateCardCommand request, CardResponse response, CancellationToken cancellationToken)
        {
            await InvalidateCacheForRequest(new GetBoardQuery() { BoardId = request.BoardId });
        }
    }
}