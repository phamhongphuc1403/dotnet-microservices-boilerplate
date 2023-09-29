using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCRM.Identity.EntityFrameworkCore.Entities;

namespace TinyCRM.Identity.EntityFrameworkCore.EntityConfigurations;

public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.HasIndex(user => user.Name).IsUnique();
        builder.HasIndex(user => user.NormalizedName).IsUnique();

        builder.Property(role => role.Name)
            .HasMaxLength(256)
            .IsRequired();
        builder.Property(role => role.NormalizedName)
            .HasMaxLength(256)
            .IsRequired();

        builder.HasMany(r => r.Claims)
            .WithOne()
            .HasForeignKey(r => r.RoleId);
    }
}