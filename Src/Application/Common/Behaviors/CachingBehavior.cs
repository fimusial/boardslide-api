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
        private readonly ICacheKeyGenerator _keyGenerator;

        public CachingBehavior(ICacheService cache, ICacheKeyGenerator keyGenerator)
        {
            _cache = cache;
            _keyGenerator = keyGenerator;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            CachedRequestAttribute attribute = (CachedRequestAttribute)Attribute
                .GetCustomAttribute(typeof(TRequest), typeof(CachedRequestAttribute));

            if (attribute == null)
            {
                return await next();
            }

            string key = _keyGenerator.GenerateCacheKeyFromRequest(request);
            TResponse cachedResponse = await _cache.GetValueAsync<TResponse>(key);
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