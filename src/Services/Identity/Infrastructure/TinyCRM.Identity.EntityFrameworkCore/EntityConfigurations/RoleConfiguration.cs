using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCRM.Identity.IdentityDomain.RoleAggregate.Entities;

namespace TinyCRM.Identity.EntityFrameworkCore.EntityConfigurations;

public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.HasIndex(user => user.NormalizedName).IsUnique().HasFilter("\"DeletedAt\" IS NULL");

        builder.Property(role => role.Name)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(role => role.NormalizedName)
            .HasMaxLength(256)
            .IsRequired();
    }
}