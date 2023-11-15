using BuildingBlock.Core.Domain;
using IdentityManagement.Core.Domain.RoleAggregate.Entities;

namespace IdentityManagement.Core.Domain.PermissionAggregate.Entities;

public class Permission : AggregateRoot
{
    public Permission(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public Permission()
    {
    }

    public Guid RoleId { get; set; }

    public Role Role { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;
}