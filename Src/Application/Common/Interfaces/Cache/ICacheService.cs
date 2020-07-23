using System;
using System.Threading.Tasks;

namespace BoardSlide.API.Application.Common.Interfaces.Cache
{
    public interface ICacheService
    {
        Task<object> GetValueAsync(string key);
        Task SetValueAsync(string key, object value, TimeSpan lifespan);
    }
}