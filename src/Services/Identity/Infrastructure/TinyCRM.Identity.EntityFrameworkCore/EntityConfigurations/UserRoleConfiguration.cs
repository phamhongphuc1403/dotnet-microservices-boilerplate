using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCRM.Identity.IdentityDomain.RoleAggregate.Entities;

namespace TinyCRM.Identity.EntityFrameworkCore.EntityConfigurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
{
    public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
    {
        builder.HasOne(userRole => userRole.User)
            .WithMany(user => user.UserRoles)
            .HasForeignKey(userRole => userRole.UserId);

        builder.HasOne(userRole => userRole.Role)
            .WithMany(role => role.UserRoles)
            .HasForeignKey(userRole => userRole.RoleId);

        builder.HasKey(userRole => userRole.Id);

        builder.Property(userRole => userRole.Id)
            .ValueGeneratedOnAdd();

        builder.HasIndex(e => new { e.UserId, e.RoleId })
            .IsUnique();
    }
}