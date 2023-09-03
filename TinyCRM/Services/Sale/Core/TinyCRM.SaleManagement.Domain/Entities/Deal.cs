using TinyCRM.Core;
using TinyCRM.SaleManagement.Domain.Entities.Enums;

namespace TinyCRM.SaleManagement.Domain.Entities;

public class Deal : GuidBaseEntity
{
    public string Title { get; private set; } = null!;
    public Guid LeadId { get; private set; }
    public virtual Lead Lead { get; private set; } = null!;
    public string? Description { get; private set; }
    public DealStatuses Status { get; private set; }
    public virtual ICollection<DealLine> DealLines { get; private set; } = new HashSet<DealLine>();
}