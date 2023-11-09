using BuildingBlock.Core.Domain;
using Identitymanagement.Core.Domain.PermissionAggregate.Entities;

namespace Identitymanagement.Core.Domain.RoleAggregate.Entities;

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