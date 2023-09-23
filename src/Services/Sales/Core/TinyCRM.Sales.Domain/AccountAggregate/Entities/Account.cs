using BuildingBlock.Domain;
using TinyCRM.Sales.Domain.LeadAggregate.Entities;

namespace TinyCRM.Sales.Domain.AccountAggregate.Entities;

public class Account : Entity
{
    public string Name { get; private set; } = null!;

    public string Email { get; private set; } = null!;
    public ICollection<Lead> Leads { get; private set; } = null!;
}