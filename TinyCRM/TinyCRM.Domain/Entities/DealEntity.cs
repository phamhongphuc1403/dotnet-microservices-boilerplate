using TinyCRM.Domain.Enums;

namespace TinyCRM.Domain.Entities;

public class DealEntity : GuidBaseEntity
{
    public string Title { get; set; } = null!;
    public Guid LeadId { get; set; }
    public virtual LeadEntity Lead { get; set; } = null!;
    public string? Description { get; set; }
    public DealStatuses Status { get; set; }
    public virtual ICollection<DealProductEntity> DealsProducts { get; set; } = new HashSet<DealProductEntity>();
}