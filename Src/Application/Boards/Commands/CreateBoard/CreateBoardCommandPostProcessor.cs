using System.Threading;
using System.Threading.Tasks;
using BoardSlide.API.Application.Boards.Queries.GetBoards;
using BoardSlide.API.Application.Boards.Responses;
using BoardSlide.API.Application.Common;
using BoardSlide.API.Application.Common.Interfaces.Cache;
using MediatR.Pipeline;

namespace BoardSlide.API.Application.Boards.Commands.CreateBoard
{
    public class CreateBoardCommandPostProcessor : CacheInvalidationPostProcessorBase,
        IRequestPostProcessor<CreateBoardCommand, BoardInfoResponse>
    {
        public CreateBoardCommandPostProcessor(ICacheService cache, ICacheKeyGenerator keyGenerator)
            : base(cache, keyGenerator)
        {
        }

        public async Task Process(CreateBoardCommand request, BoardInfoResponse response, CancellationToken cancellationToken)
        {
            await InvalidateCacheForRequest(new GetBoardsQuery());
        }
    }
}