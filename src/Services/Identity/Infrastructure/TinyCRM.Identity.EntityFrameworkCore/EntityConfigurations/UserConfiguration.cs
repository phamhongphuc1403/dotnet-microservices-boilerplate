using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCRM.Identity.IdentityDomain.UserAggregate.Entities;

namespace TinyCRM.Identity.EntityFrameworkCore.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasIndex(user => user.NormalizedEmail).IsUnique().HasFilter("\"DeletedAt\" IS NULL");
        builder.HasIndex(user => user.NormalizedUserName).IsUnique().HasFilter("\"DeletedAt\" IS NULL");

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