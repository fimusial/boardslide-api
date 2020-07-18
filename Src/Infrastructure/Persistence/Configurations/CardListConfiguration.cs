using BoardSlide.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardSlide.API.Infrastructure.Persistence.Configurations
{
    public class CardListConfiguration : IEntityTypeConfiguration<CardList>
    {
        public void Configure(EntityTypeBuilder<CardList> builder)
        {
            builder.HasKey(list => list.Id);

            builder.Property(list => list.Name)
                .IsRequired()
                .HasMaxLength(32);

            builder.HasOne(list => list.Board)
                .WithMany(board => board.CardLists)
                .HasForeignKey(list => list.BoardId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}