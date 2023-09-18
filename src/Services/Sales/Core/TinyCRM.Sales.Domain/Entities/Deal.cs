using BuildingBlock.Domain;
using TinyCRM.Sales.Domain.Entities.Enums;

namespace TinyCRM.Sales.Domain.Entities;

public class Deal : GuidEntity
{
    public string Title { get; private set; } = null!;
    public Guid LeadId { get; private set; }
    public virtual Lead Lead { get; private set; } = null!;
    public string? Description { get; private set; }
    public DealStatus Status { get; private set; }
    public virtual ICollection<DealLine> DealLines { get; private set; } = new HashSet<DealLine>();
}