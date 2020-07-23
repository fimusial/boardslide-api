using System;

namespace BoardSlide.API.Application.Common.Interfaces.Cache
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class CachedRequestAttribute : Attribute
    {
        private readonly TimeSpan _lifespan;
        public int LifespanInSeconds => (int)_lifespan.TotalSeconds;

        public CachedRequestAttribute(int lifespanInSeconds)
        {
            _lifespan = TimeSpan.FromSeconds(lifespanInSeconds);
        }
    }
}