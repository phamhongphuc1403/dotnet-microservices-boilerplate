using IdentityManagement.Infrastructure.Identity.PermissionAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityManagement.Infrastructure.EntityFrameworkCore.EntityConfigurations;

public class PermissionConfiguration : IEntityTypeConfiguration<ApplicationPermission>
{
    public void Configure(EntityTypeBuilder<ApplicationPermission> builder)
    {
        builder.Property(permissions => permissions.ClaimType)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(permissions => permissions.ClaimValue)
            .HasMaxLength(256)
            .IsRequired();

        builder.HasIndex(permissions => new { permissions.RoleId, permissions.ClaimType })
            .IsUnique()
            .HasFilter("\"DeletedAt\" IS NULL");

        builder.HasOne(permission => permission.Role)
            .WithMany(role => role.Permissions)
            .HasForeignKey(permission => permission.RoleId);

        builder.HasKey(permission => permission.Id);
    }
}