using BoardSlide.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardSlide.API.Infrastructure.Persistence.Configurations
{
    public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder.HasKey(item => item.Id);

            builder.Property(item => item.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(item => item.Description)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}