using BuildingBlock.Domain;

namespace TinyCRM.Identity.Domain.RoleAggregate.Entities;

public class Role : Entity
{
    public string Name { get; set; } = null!;
}