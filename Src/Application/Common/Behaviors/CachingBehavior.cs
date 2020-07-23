using System;
using System.Threading;
using System.Threading.Tasks;
using BoardSlide.API.Application.Common.Interfaces.Cache;
using MediatR;

namespace BoardSlide.API.Application.Common.Behaviors
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseRequest
    {
        private readonly ICacheService _cache;

        public CachingBehavior(ICacheService cache)
        {
            _cache = cache;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var attribute = (CachedRequestAttribute)Attribute.GetCustomAttribute(typeof(TRequest), typeof(CachedRequestAttribute));
            if (attribute == null)
            {
                return await next();
            }

            string key = CacheKeyGenerator.GenerateCacheKeyFromRequest(request);
            var cachedResponse = (TResponse)await _cache.GetValueAsync(key);
            if (cachedResponse != null)
            {
                return cachedResponse;
            }
            
            TResponse handlerResponse = await next();
            await _cache.SetValueAsync(key, handlerResponse, TimeSpan.FromSeconds(attribute.LifespanInSeconds));
            return handlerResponse;
        }
    }
}