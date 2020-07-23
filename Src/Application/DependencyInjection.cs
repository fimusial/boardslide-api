using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using BoardSlide.API.Application.Common.Behaviors;
using BoardSlide.API.Application.Common.Settings;
using FluentValidation;
using MediatR;

namespace BoardSlide.API.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assembly);

            services.AddMediatR(assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));

            services.Configure<ApplicationSettings>(options => configuration.GetSection("ApplicationSettings").Bind(options));
            return services;
        }
    }
}