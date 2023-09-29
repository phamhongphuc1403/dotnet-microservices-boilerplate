using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TinyCRM.Identities.EntityFrameworkCore.EntityConfigurations;

public class RoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
    {
        builder.Property(roleClaim => roleClaim.ClaimType)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(roleClaim => roleClaim.ClaimValue)
            .HasMaxLength(256)
            .IsRequired();

        builder.HasIndex(roleClaim => new { roleClaim.RoleId, roleClaim.ClaimType }).IsUnique();
    }
}