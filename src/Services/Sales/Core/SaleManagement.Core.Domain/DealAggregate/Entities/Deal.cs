using BuildingBlock.Core.Domain;
using SaleManagement.Core.Domain.DealAggregate.Entities.Enums;
using SaleManagement.Core.Domain.LeadAggregate.Entities;

namespace SaleManagement.Core.Domain.DealAggregate.Entities;

public class Deal : AggregateRoot
{
    public string Title { get; private set; } = null!;
    public Guid LeadId { get; }
    public Lead Lead { get; private set; } = null!;
    public string? Description { get; }
    public DealStatus Status { get; }
    public ICollection<DealLine> DealLines { get; private set; } = null!;
}