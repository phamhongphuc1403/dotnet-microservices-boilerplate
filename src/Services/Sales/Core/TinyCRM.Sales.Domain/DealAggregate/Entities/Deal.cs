using BuildingBlock.Domain;
using TinyCRM.Sales.Domain.DealAggregate.Entities.Enums;
using TinyCRM.Sales.Domain.LeadAggregate.Entities;

namespace TinyCRM.Sales.Domain.DealAggregate.Entities;

public class Deal : Entity
{
    public string Title { get; private set; } = null!;
    public Guid LeadId { get; private set; }
    public virtual Lead Lead { get; private set; } = null!;
    public string? Description { get; private set; }
    public DealStatus Status { get; private set; }
    public virtual ICollection<DealLine> DealLines { get; private set; } = new HashSet<DealLine>();
}