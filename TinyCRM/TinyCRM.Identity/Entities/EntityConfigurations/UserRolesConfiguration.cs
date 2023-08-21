using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCRM.Identity.Entities.Constants;

namespace TinyCRM.Identity.Entities.EntityConfigurations;

public class UserRolesConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                RoleId = RoleId.Admin,
                UserId = UserId.Admin
            },
            new IdentityUserRole<string>
            {
                RoleId = RoleId.SuperAdmin,
                UserId = UserId.SuperAdmin
            },
            new IdentityUserRole<string>
            {
                RoleId = RoleId.User,
                UserId = UserId.User
            }
        );
    }
}