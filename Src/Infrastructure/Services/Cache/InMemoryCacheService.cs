using System;
using System.Threading.Tasks;
using BoardSlide.API.Application.Common.Interfaces.Cache;
using Microsoft.Extensions.Caching.Memory;

namespace BoardSlide.API.Infrastructure.Services.Cache
{
    public class InMemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public InMemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Task<object> GetValueAsync(string key)
        {
            if (_memoryCache.TryGetValue<object>(key, out var value))
            {
                return Task.FromResult(value);
            }

            return Task.FromResult<object>(null);
        }

        public Task SetValueAsync(string key, object value, TimeSpan lifespan)
        {
            _memoryCache.Set(key, value, absoluteExpirationRelativeToNow: lifespan);
            return Task.CompletedTask;
        }
    }
}