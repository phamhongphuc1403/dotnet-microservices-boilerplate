using BuildingBlock.Core.Domain;
using SaleManagement.Core.Domain.DealAggregate.Entities;

namespace SaleManagement.Core.Domain.ProductAggregate.Entities;

public class Product : AggregateRoot
{
    public string Code { get; private set; } = null!;
    public ICollection<DealLine> DealLines { get; private set; } = null!;
}