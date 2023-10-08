using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCRM.Identity.Identity.Entities;

namespace TinyCRM.Identity.EntityFrameworkCore.EntityConfigurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<ApplicationRefreshToken>
{
    public void Configure(EntityTypeBuilder<ApplicationRefreshToken> builder)
    {
        builder.Property(refreshToken => refreshToken.UserId)
            .IsRequired();

        builder.Property(refreshToken => refreshToken.Token)
            .IsRequired();

        builder.Property(refreshToken => refreshToken.DeviceId)
            .HasMaxLength(256)
            .IsRequired();

        builder.HasOne(refreshToken => refreshToken.ApplicationUser)
            .WithMany(user => user.RefreshTokens)
            .HasForeignKey(refreshToken => refreshToken.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.ToTable("UserRefreshTokens");

    }
}