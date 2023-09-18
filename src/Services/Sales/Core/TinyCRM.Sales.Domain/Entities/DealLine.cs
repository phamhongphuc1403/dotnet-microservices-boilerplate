using BuildingBlock.Domain;

namespace TinyCRM.Sales.Domain.Entities;

public class DealLine : GuidEntity
{
    public Guid ProductId { get; private set; }
    public virtual Product Product { get; private set; } = null!;
    public Guid DealId { get; private set; }
    public virtual Deal Deal { get; private set; } = null!;
    public int Quantity { get; private set; }
    public double PricePerUnit { get; private set; }
}