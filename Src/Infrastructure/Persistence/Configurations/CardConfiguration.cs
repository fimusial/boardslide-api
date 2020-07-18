using BoardSlide.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardSlide.API.Infrastructure.Persistence.Configurations
{
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(card => card.Id);

            builder.Property(card => card.Name)
                .IsRequired()
                .HasMaxLength(32);

            builder.Property(card => card.Description)
                .IsRequired(false)
                .HasMaxLength(255);

            builder.Property(card => card.DueDate)
                .IsRequired(false);

            builder.HasOne(card => card.CardList)
                .WithMany(list => list.Cards)
                .HasForeignKey(card => card.CardListId);
        }
    }
}