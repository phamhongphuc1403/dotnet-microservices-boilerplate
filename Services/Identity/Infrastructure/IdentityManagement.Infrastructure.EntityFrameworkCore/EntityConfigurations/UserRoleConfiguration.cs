using IdentityManagement.Infrastructure.Identity.RoleAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityManagement.Infrastructure.EntityFrameworkCore.EntityConfigurations;

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

        builder.HasIndex(userRole => new { userRole.UserId, userRole.RoleId })
            .IsUnique()
            .HasFilter("\"DeletedAt\" IS NULL");
    }
}