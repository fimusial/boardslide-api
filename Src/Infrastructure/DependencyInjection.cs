using System;
using System.Text;
using BoardSlide.API.Application.Common.Interfaces;
using BoardSlide.API.Application.Common.Interfaces.Repositories;
using BoardSlide.API.Infrastructure.Identity;
using BoardSlide.API.Infrastructure.Identity.Entities;
using BoardSlide.API.Infrastructure.Identity.Settings;
using BoardSlide.API.Infrastructure.Persistence;
using BoardSlide.API.Infrastructure.Repositories;
using BoardSlide.API.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BoardSlide.API.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("Default");
            int maxRetryCount = configuration.GetSection("DatabaseSettings").GetValue<int>("ConnectRetryCount");

            services.AddDbContext<ApplicationDbContext>(options => options
                .UseSqlServer(connectionString, builder => builder.EnableRetryOnFailure(maxRetryCount)));

            services.AddDbContext<IdentityDbContext>(options => options
                .UseSqlServer(connectionString, builder => builder.EnableRetryOnFailure(maxRetryCount)));

            services.AddIdentityCore<ApplicationUser>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddEntityFrameworkStores<IdentityDbContext>();
            
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
                options.User.RequireUniqueEmail = false;
            });

            services.Configure<JwtSettings>(options => configuration.GetSection("JwtSettings").Bind(options));
            string secret = configuration["JwtSettings:Secret"];

            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)),
                ValidateLifetime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = tokenValidationParameters;
            });

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IBoardsRepository, BoardsRepository>();
            services.AddScoped<ICardListsRepository, CardListsRepository>();
            services.AddScoped<ICardsRepository, CardsRepository>();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddSingleton<TokenValidationParameters>(tokenValidationParameters);
            return services;
        }
    }
}