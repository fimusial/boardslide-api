using System.ComponentModel.DataAnnotations.Schema;
using BoardSlide.API.Infrastructure.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardSlide.API.Infrastructure.Identity.Configurations
{
    public class RefreshTokenInfoConfiguration : IEntityTypeConfiguration<RefreshTokenInfo>
    {
        public void Configure(EntityTypeBuilder<RefreshTokenInfo> builder)
        {
            builder.HasKey(token => token.Token);

            builder.Property(token => token.Token)
                .ValueGeneratedOnAdd();

            builder.HasOne(token => token.User)
                .WithMany(user => user.RefreshTokens)
                .HasForeignKey(token => token.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}