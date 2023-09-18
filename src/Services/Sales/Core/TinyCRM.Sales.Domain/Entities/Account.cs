using BuildingBlock.Domain;

namespace TinyCRM.Sales.Domain.Entities;

public class Account : GuidEntity
{
    public string Name { get; private set; } = null!;
    public virtual ICollection<Lead> Leads { get; private set; } = new HashSet<Lead>();
}