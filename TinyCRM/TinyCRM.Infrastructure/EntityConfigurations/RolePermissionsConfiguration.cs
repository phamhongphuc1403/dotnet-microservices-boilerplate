using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCRM.Domain.Constants;

namespace TinyCRM.Infrastructure.EntityConfigurations;

public class RolePermissionsConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
    {
        builder.HasIndex(u => new { u.RoleId, u.ClaimType }).IsUnique();

        var index = 1;

        foreach (var permission in Permission.PermissionsList)
        {
            builder.HasData(new IdentityRoleClaim<string>
            {
                Id = index++,
                RoleId = "d8bc22dc-5c2d-41c7-bc22-6293121a1ce1",
                ClaimType = permission.Type,
                ClaimValue = permission.Value
            });

            if (permission.Type == Permission.Role.Update) continue;

            builder.HasData(new IdentityRoleClaim<string>
            {
                Id = index++,
                RoleId = "80bee362-64ca-42cc-aeb2-444d5f61b008",
                ClaimType = permission.Type,
                ClaimValue = permission.Value
            });

            if (permission.Type
                is Permission.User.ViewPersonal
                or Permission.User.UpdatePersonal
                or Permission.Account.View
                or Permission.Contact.View
                or Permission.Lead.View
                or Permission.Deal.View
                or Permission.Product.View)
                builder.HasData(new IdentityRoleClaim<string>
                {
                    Id = index++,
                    RoleId = "d8bc22dc-5c2d-41c7-bc22-6293121a1cef",
                    ClaimType = permission.Type,
                    ClaimValue = permission.Value
                });
        }
    }
}