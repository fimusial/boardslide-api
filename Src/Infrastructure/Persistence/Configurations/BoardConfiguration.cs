using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BoardSlide.API.Domain.Entities;

namespace BoardSlide.API.Infrastructure.Persistence.Configurations
{
    public class BoardConfiguration : IEntityTypeConfiguration<Board>
    {
        public void Configure(EntityTypeBuilder<Board> builder)
        {
            builder.HasKey(board => board.Id);

            builder.Property(board => board.Name)
                .IsRequired()
                .HasMaxLength(32);

            builder.HasIndex(board => board.OwnerId)
                .IsUnique(false);

            builder.Property(board => board.OwnerId)
                .IsRequired();
        }
    }
}