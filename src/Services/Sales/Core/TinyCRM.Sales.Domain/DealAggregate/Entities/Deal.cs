using BuildingBlock.Domain;
using TinyCRM.Sales.Domain.DealAggregate.Entities.Enums;
using TinyCRM.Sales.Domain.LeadAggregate.Entities;

namespace TinyCRM.Sales.Domain.DealAggregate.Entities;

public class Deal : Entity
{
    public string Title { get; private set; } = null!;
    public Guid LeadId { get; }
    public Lead Lead { get; private set; } = null!;
    public string? Description { get; }
    public DealStatus Status { get; }
    public ICollection<DealLine> DealLines { get; private set; } = null!;
}