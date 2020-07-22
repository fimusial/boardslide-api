using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BoardSlide.API.Infrastructure.Identity;
using BoardSlide.API.Infrastructure.Persistence;

namespace BoardSlide.API.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var applicationContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    var identityContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
                    applicationContext.Database.Migrate();
                    identityContext.Database.Migrate();
                }
                catch (Exception)
                {
                    // log exception
                    throw;
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
