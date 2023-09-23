using BuildingBlock.Domain;
using TinyCRM.Sales.Domain.ProductAggregate.Entities;

namespace TinyCRM.Sales.Domain.DealAggregate.Entities;

public class DealLine : Entity
{
    public Guid ProductId { get; private set; }
    public virtual Product Product { get; private set; } = null!;
    public Guid DealId { get; private set; }
    public virtual Deal Deal { get; private set; } = null!;
    public int Quantity { get; private set; }
    public double PricePerUnit { get; private set; }
}