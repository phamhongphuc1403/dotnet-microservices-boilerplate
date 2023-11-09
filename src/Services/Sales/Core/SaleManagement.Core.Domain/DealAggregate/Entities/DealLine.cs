using BuildingBlock.Core.Domain;
using SaleManagement.Core.Domain.ProductAggregate.Entities;

namespace SaleManagement.Core.Domain.DealAggregate.Entities;

public class DealLine : Entity
{
    public Guid ProductId { get; }
    public Product Product { get; private set; } = null!;
    public Guid DealId { get; }
    public Deal Deal { get; private set; } = null!;
    public int Quantity { get; }
    public double PricePerUnit { get; }
}