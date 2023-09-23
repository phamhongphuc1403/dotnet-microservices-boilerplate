using BuildingBlock.Domain;
using TinyCRM.Sales.Domain.DealAggregate.Entities;

namespace TinyCRM.Sales.Domain.ProductAggregate.Entities;

public class Product : Entity
{
    public string Code { get; private set; } = null!;
    public virtual ICollection<DealLine> DealLines { get; private set; } = new HashSet<DealLine>();
}