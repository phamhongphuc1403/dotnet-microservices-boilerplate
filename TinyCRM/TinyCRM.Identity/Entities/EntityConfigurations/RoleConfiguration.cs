using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCRM.Domain.Constants;
using TinyCRM.Identity.Entities.Constants;

namespace TinyCRM.Identity.Entities.EntityConfigurations;

public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.HasData(
            new ApplicationRole
            {
                Id = RoleId.Admin,
                Name = Role.Admin,
                NormalizedName = Role.Admin.ToUpper()
            },
            new ApplicationRole
            {
                Id = RoleId.User,
                Name = Role.User,
                NormalizedName = Role.User.ToUpper()
            },
            new ApplicationRole
            {
                Id = RoleId.SuperAdmin,
                Name = Role.SuperAdmin,
                NormalizedName = Role.SuperAdmin.ToUpper()
            }
        );

        builder.HasMany(r => r.Claims)
            .WithOne()
            .HasForeignKey(r => r.RoleId);
    }
}