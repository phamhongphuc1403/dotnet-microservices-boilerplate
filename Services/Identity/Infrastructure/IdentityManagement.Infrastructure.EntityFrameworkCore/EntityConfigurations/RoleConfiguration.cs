using IdentityManagement.Infrastructure.Identity.RoleAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityManagement.Infrastructure.EntityFrameworkCore.EntityConfigurations;

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