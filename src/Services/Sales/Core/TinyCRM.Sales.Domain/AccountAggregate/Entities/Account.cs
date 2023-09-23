using BuildingBlock.Domain;
using TinyCRM.Sales.Domain.LeadAggregate.Entities;

namespace TinyCRM.Sales.Domain.AccountAggregate.Entities;

public class Account : Entity
{
    public string Name { get; private set; } = null!;
    public virtual ICollection<Lead> Leads { get; private set; } = new HashSet<Lead>();
}