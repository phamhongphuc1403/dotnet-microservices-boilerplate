using BuildingBlock.Domain;
using TinyCRM.Identity.Domain.RoleAggregate.Entities;

namespace TinyCRM.Identity.Domain.PermissionAggregate.Entities;

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