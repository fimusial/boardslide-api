using BoardSlide.API.Application.Common.Interfaces;
using BoardSlide.API.Domain.Entities;
using BoardSlide.API.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BoardSlide.API.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new TodoItemConfiguration());
        }
    }
}