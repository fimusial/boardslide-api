using System.Reflection;
using System.Text;
using BoardSlide.API.Application.Common.Interfaces.Cache;
using MediatR;

namespace BoardSlide.API.Infrastructure.Services.Cache
{
    public class CacheKeyGenerator : ICacheKeyGenerator
    {
        public string GenerateCacheKeyFromRequest<TRequest>(TRequest request) where TRequest : IBaseRequest
        {
            var key = new StringBuilder();

            key.Append($"{typeof(TRequest).Name}");
            foreach (PropertyInfo property in request.GetType().GetProperties())
            {
                key.Append($"/{property.Name}:{property.GetValue(request)}");
            }

            return key.ToString();
        }
    }
}