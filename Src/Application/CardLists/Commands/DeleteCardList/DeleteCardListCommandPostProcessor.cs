using System.Threading;
using System.Threading.Tasks;
using BoardSlide.API.Application.Boards.Queries.GetBoard;
using BoardSlide.API.Application.Common;
using BoardSlide.API.Application.Common.Interfaces.Cache;
using MediatR;
using MediatR.Pipeline;

namespace BoardSlide.API.Application.CardLists.Commands.DeleteCardList
{
    public class DeleteCardListCommandPostProcessor : CacheInvalidationPostProcessorBase,
        IRequestPostProcessor<DeleteCardListCommand, Unit>
    {
        public DeleteCardListCommandPostProcessor(ICacheService cache, ICacheKeyGenerator keyGenerator)
            : base(cache, keyGenerator)
        {
        }

        public async Task Process(DeleteCardListCommand request, Unit response, CancellationToken cancellationToken)
        {
            await InvalidateCacheForRequest(new GetBoardQuery() { BoardId = request.BoardId });
        }
    }
}