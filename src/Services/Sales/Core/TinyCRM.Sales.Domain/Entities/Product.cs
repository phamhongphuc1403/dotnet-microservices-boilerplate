using BuildingBlock.Domain;

namespace TinyCRM.Sales.Domain.Entities;

public class Product : GuidBaseEntity
{
    public string Code { get; private set; } = null!;
    public virtual ICollection<DealLine> DealLines { get; private set; } = new HashSet<DealLine>();
}