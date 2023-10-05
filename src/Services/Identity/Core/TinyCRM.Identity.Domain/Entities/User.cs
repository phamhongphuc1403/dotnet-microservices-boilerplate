using BuildingBlock.Domain;

namespace TinyCRM.Identities.Domain.Entities;

public class User : AggregateRoot
{
    public string Email { get; set; } = null!;
}