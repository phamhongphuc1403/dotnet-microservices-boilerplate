using BuildingBlock.Domain;

namespace TinyCRM.Identities.Domain.RoleAggregate.Entities;

public class Role : Entity
{
    public string Name { get; set; } = null!;
}