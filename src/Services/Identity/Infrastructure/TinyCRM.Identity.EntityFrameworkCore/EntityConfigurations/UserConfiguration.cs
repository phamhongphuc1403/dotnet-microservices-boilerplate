using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCRM.Identity.EntityFrameworkCore.Entities;

namespace TinyCRM.Identity.EntityFrameworkCore.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasIndex(user => user.Email).IsUnique();
        builder.Property(user => user.NormalizedEmail).IsRequired();
        builder.Property(user => user.UserName).IsRequired();
        builder.Property(user => user.NormalizedUserName).IsRequired();

        builder.Property(user => user.Email)
            .HasMaxLength(320)
            .IsRequired();
        builder.Property(user => user.NormalizedEmail)
            .HasMaxLength(320)
            .IsRequired();
        builder.Property(user => user.UserName)
            .HasMaxLength(320)
            .IsRequired();
        builder.Property(user => user.NormalizedUserName)
            .HasMaxLength(320)
            .IsRequired();
        builder.Property(user => user.PasswordHash)
            .IsRequired();
    }
}