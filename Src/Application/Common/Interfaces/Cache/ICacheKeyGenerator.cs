using MediatR;

namespace BoardSlide.API.Application.Common.Interfaces.Cache
{
    public interface ICacheKeyGenerator
    {
        string GenerateCacheKeyFromRequest<TRequest>(TRequest request) where TRequest : IBaseRequest;
    }
}