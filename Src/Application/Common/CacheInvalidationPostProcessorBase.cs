using System.Threading.Tasks;
using BoardSlide.API.Application.Common.Interfaces.Cache;
using MediatR;

namespace BoardSlide.API.Application.Common
{
    public class CacheInvalidationPostProcessorBase
    {
        protected readonly ICacheService _cache;
        protected readonly ICacheKeyGenerator _keyGenerator;

        protected CacheInvalidationPostProcessorBase(ICacheService cache, ICacheKeyGenerator keyGenerator)
        {
            _cache = cache;
            _keyGenerator = keyGenerator;
        }

        protected async Task InvalidateCacheForRequest<TRequest>(TRequest request) where TRequest : IBaseRequest
        {
            await _cache.RemoveValueAsync(_keyGenerator.GenerateCacheKeyFromRequest(request));
        }
    }
}