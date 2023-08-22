using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyCRM.Domain.Constants;
using TinyCRM.Domain.Entities;
using TinyCRM.Identity.Entities.Constants;

namespace TinyCRM.Identity.Entities.EntityConfigurations;

public class RolePermissionsConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
    {
        builder.HasIndex(u => new { u.RoleId, u.ClaimType }).IsUnique();

        var index = 1;

        foreach (var permission in Permission.PermissionsList)
        {
            index = SeedSuperAdmin(builder, permission, index);

            index = SeedAdmin(builder, permission, index);

            index = SeedUser(builder, permission, index);
        }
    }

    private static int SeedData(
        EntityTypeBuilder<IdentityRoleClaim<string>> builder, PermissionEntity permission, int index, string roleId)
    {
        builder.HasData(new IdentityRoleClaim<string>
        {
            Id = index,
            RoleId = roleId,
            ClaimType = permission.Type,
            ClaimValue = permission.Value
        });

        return ++index;
    }

    private static int SeedSuperAdmin(
        EntityTypeBuilder<IdentityRoleClaim<string>> builder, PermissionEntity permission, int index)
    {
        return SeedData(builder, permission, index, RoleId.SuperAdmin);
    }

    private static int SeedAdmin(
        EntityTypeBuilder<IdentityRoleClaim<string>> builder, PermissionEntity permission, int index)
    {
        return permission.Type != Permission.Role.Update
            ? SeedData(builder, permission, index, RoleId.Admin)
            : index;
    }

    private static int SeedUser(
        EntityTypeBuilder<IdentityRoleClaim<string>> builder, PermissionEntity permission, int index)
    {
        return permission.Type
            is Permission.User.ViewPersonal
            or Permission.User.UpdatePersonal
            or Permission.Account.View
            or Permission.Contact.View
            or Permission.Lead.View
            or Permission.Deal.View
            or Permission.Product.View
            ? SeedData(builder, permission, index, RoleId.User)
            : index;
    }
}