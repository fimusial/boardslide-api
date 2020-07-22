using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BoardSlide.API.Infrastructure.Identity.Configurations;
using BoardSlide.API.Infrastructure.Identity.Entities;

namespace BoardSlide.API.Infrastructure.Identity
{
    public class IdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<RefreshTokenInfo> RefreshTokens { get; set; }

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RefreshTokenInfoConfiguration());
        }
    }
}