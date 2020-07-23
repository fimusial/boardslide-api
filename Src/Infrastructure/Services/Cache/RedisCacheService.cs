using System;
using System.Threading.Tasks;
using BoardSlide.API.Application.Common.Interfaces.Cache;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace BoardSlide.API.Infrastructure.Services.Cache
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly JsonSerializerSettings _settings;

        public RedisCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;

            _settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                NullValueHandling = NullValueHandling.Include,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }

        public async Task<object> GetValueAsync(string key)
        {
            string value = await _distributedCache.GetStringAsync(key);
            if (value == null)
            {
                return await Task.FromResult<object>(null);
            }

            object deserializedValue = JsonConvert.DeserializeObject(value, _settings);
            return await Task.FromResult(deserializedValue);
        }

        public async Task SetValueAsync(string key, object value, TimeSpan lifespan)
        {
            string serializedValue = JsonConvert.SerializeObject(value, _settings);

            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = lifespan
            };

            await _distributedCache.SetStringAsync(key, serializedValue, cacheOptions);
        }
    }
}