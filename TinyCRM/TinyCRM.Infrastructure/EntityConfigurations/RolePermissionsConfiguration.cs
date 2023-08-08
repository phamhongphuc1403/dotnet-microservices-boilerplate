using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TinyCRM.Domain.Entities;
using System.Reflection;
using TinyCRM.Domain.Constants.Permissions;

namespace TinyCRM.Infrastructure.EntityConfigurations
{
    internal class RolePermissionsConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
        {
            builder.HasIndex(u => new { u.RoleId, u.ClaimType }).IsUnique();

            var index = 1;

            foreach (Type permissionType in typeof(Permissions).GetNestedTypes())
            {
                PropertyInfo[] properties = permissionType.GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    if (property.PropertyType == typeof(PermissionContent))
                    {
                        var permission = (PermissionContent)property.GetValue(null)!;

                        builder.HasData(new IdentityRoleClaim<string>
                        {
                            Id = index++,
                            RoleId = "80bee362-64ca-42cc-aeb2-444d5f61b008",
                            ClaimType = permission.Name,
                            ClaimValue = permission.Description
                        });

                        if (property.Name is "ViewPersonal" or "View" or "UpdatePersonal")
                        {
                            builder.HasData(new IdentityRoleClaim<string>
                            {
                                Id = index++,
                                RoleId = "d8bc22dc-5c2d-41c7-bc22-6293121a1cef",
                                ClaimType = permission.Name,
                                ClaimValue = permission.Description
                            });
                        }
                    }
                }
            }
        }
    }
}
