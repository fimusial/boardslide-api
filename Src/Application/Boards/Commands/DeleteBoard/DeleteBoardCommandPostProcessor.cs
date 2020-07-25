using System.Threading;
using System.Threading.Tasks;
using BoardSlide.API.Application.Boards.Queries.GetBoard;
using BoardSlide.API.Application.Boards.Queries.GetBoards;
using BoardSlide.API.Application.Common;
using BoardSlide.API.Application.Common.Interfaces.Cache;
using MediatR;
using MediatR.Pipeline;

namespace BoardSlide.API.Application.Boards.Commands.DeleteBoard
{
    public class DeleteBoardCommandPostProcessor : CacheInvalidationPostProcessorBase,
        IRequestPostProcessor<DeleteBoardCommand, Unit>
    {
        public DeleteBoardCommandPostProcessor(ICacheService cache, ICacheKeyGenerator keyGenerator)
            : base(cache, keyGenerator)
        {
        }

        public async Task Process(DeleteBoardCommand request, Unit response, CancellationToken cancellationToken)
        {
            await InvalidateCacheForRequest(new GetBoardQuery() { BoardId = request.BoardId });
            await InvalidateCacheForRequest(new GetBoardsQuery());
        }
    }
}