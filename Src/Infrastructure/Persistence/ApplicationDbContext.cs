using Microsoft.EntityFrameworkCore;
using BoardSlide.API.Domain.Entities;
using BoardSlide.API.Infrastructure.Persistence.Configurations;

namespace BoardSlide.API.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Board> Boards { get; set; }
        public DbSet<CardList> CardLists { get; set; }
        public DbSet<Card> Cards { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new BoardConfiguration());
            builder.ApplyConfiguration(new CardListConfiguration());
            builder.ApplyConfiguration(new CardConfiguration());
        }
    }
}