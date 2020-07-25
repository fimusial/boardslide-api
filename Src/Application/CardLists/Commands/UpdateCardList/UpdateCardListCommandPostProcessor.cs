using System.Threading;
using System.Threading.Tasks;
using BoardSlide.API.Application.Boards.Queries.GetBoard;
using BoardSlide.API.Application.CardLists.Responses;
using BoardSlide.API.Application.Common;
using BoardSlide.API.Application.Common.Interfaces.Cache;
using MediatR.Pipeline;

namespace BoardSlide.API.Application.CardLists.Commands.UpdateCardList
{
    public class UpdateCardListCommandPostProcessor : CacheInvalidationPostProcessorBase,
        IRequestPostProcessor<UpdateCardListCommand, CardListInfoResponse>
    {
        public UpdateCardListCommandPostProcessor(ICacheService cache, ICacheKeyGenerator keyGenerator)
            : base(cache, keyGenerator)
        {
        }

        public async Task Process(UpdateCardListCommand request, CardListInfoResponse response, CancellationToken cancellationToken)
        {
            await InvalidateCacheForRequest(new GetBoardQuery() { BoardId = request.BoardId });
        }
    }
}