using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCRM.Identity.IdentityDomain.PermissionAggregate.Entities;

namespace TinyCRM.Identity.EntityFrameworkCore.EntityConfigurations;

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

        builder.HasIndex(permissions => new { permissions.RoleId, permissions.ClaimType }).IsUnique();

        builder.HasOne(permission => permission.Role)
            .WithMany(role => role.Permissions)
            .HasForeignKey(permission => permission.RoleId);

        builder.HasKey(permission => permission.Id);
    }
}