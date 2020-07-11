using System;
using BoardSlide.API.Application.Common.Interfaces;

namespace BoardSlide.API.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.UtcNow;
    }
}