using System.Threading;
using System.Threading.Tasks;
using BoardSlide.API.Application.Boards.Queries.GetBoard;
using BoardSlide.API.Application.Boards.Queries.GetBoards;
using BoardSlide.API.Application.Boards.Responses;
using BoardSlide.API.Application.Common;
using BoardSlide.API.Application.Common.Interfaces.Cache;
using MediatR.Pipeline;

namespace BoardSlide.API.Application.Boards.Commands.UpdateBoard
{
    public class UpdateBoardCommandPostProcessor : CacheInvalidationPostProcessorBase,
        IRequestPostProcessor<UpdateBoardCommand, BoardInfoResponse>
    {
        public UpdateBoardCommandPostProcessor(ICacheService cache, ICacheKeyGenerator keyGenerator)
            : base(cache, keyGenerator)
        {
        }

        public async Task Process(UpdateBoardCommand request, BoardInfoResponse response, CancellationToken cancellationToken)
        {
            await InvalidateCacheForRequest(new GetBoardQuery() { BoardId = request.BoardId });
            await InvalidateCacheForRequest(new GetBoardsQuery());
        }
    }
}