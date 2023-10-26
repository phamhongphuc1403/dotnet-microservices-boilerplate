using BuildingBlock.Domain;
using TinyCRM.Identity.Domain.PermissionAggregate.Entities;

namespace TinyCRM.Identity.Domain.RoleAggregate.Entities;

public class Role : Entity
{
    public Role(string roleName)
    {
        Name = roleName;
    }

    public Role()
    {
    }

    public string Name { get; set; } = null!;

    public string ConcurrencyStamp { get; set; } = null!;

    public ICollection<UserRole> UserRoles { get; set; } = null!;

    public ICollection<Permission> Permissions { get; set; } = null!;
}