using System.Reflection;
using System.Text;
using MediatR;

namespace BoardSlide.API.Application.Common.Behaviors
{
    public static class CacheKeyGenerator
    {
        public static string GenerateCacheKeyFromRequest<TRequest>(TRequest request) where TRequest : IBaseRequest
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