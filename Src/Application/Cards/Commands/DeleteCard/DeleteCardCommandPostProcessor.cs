using System.Threading;
using System.Threading.Tasks;
using BoardSlide.API.Application.Boards.Queries.GetBoard;
using BoardSlide.API.Application.Common;
using BoardSlide.API.Application.Common.Interfaces.Cache;
using MediatR;
using MediatR.Pipeline;

namespace BoardSlide.API.Application.Cards.Commands.DeleteCard
{
    public class DeleteCardCommandPostProcessor : CacheInvalidationPostProcessorBase,
        IRequestPostProcessor<DeleteCardCommand, Unit>
    {
        public DeleteCardCommandPostProcessor(ICacheService cache, ICacheKeyGenerator keyGenerator)
            : base(cache, keyGenerator)
        {
        }

        public async Task Process(DeleteCardCommand request, Unit response, CancellationToken cancellationToken)
        {
            await InvalidateCacheForRequest(new GetBoardQuery() { BoardId = request.BoardId });
        }
    }
}