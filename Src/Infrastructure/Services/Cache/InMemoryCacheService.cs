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

        public Task<TValue> GetValueAsync<TValue>(string key)
        {
            if (_memoryCache.TryGetValue<TValue>(key, out var value))
            {
                return Task.FromResult<TValue>(value);
            }

            return Task.FromResult<TValue>(default(TValue));
        }

        public Task SetValueAsync(string key, object value, TimeSpan lifespan)
        {
            _memoryCache.Set(key, value, absoluteExpirationRelativeToNow: lifespan);
            return Task.CompletedTask;
        }

        public Task RemoveValueAsync(string key)
        {
            _memoryCache.Remove(key);
            return Task.CompletedTask;
        }
    }
}