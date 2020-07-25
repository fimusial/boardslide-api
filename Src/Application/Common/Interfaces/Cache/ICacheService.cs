using System;
using System.Threading.Tasks;

namespace BoardSlide.API.Application.Common.Interfaces.Cache
{
    public interface ICacheService
    {
        Task<TValue> GetValueAsync<TValue>(string key);
        Task SetValueAsync(string key, object value, TimeSpan lifespan);
        Task RemoveValueAsync(string key);
    }
}