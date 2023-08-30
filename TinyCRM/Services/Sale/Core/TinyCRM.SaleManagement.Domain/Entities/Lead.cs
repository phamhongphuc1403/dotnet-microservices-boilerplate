using TinyCRM.Core;
using TinyCRM.SaleManagement.Domain.Entities.Enums;

namespace TinyCRM.SaleManagement.Domain.Entities;

public class Lead : GuidBaseEntity
{
    public string Title { get; private set; } = null!;
    public Guid CustomerId { get; private set; }
    // public virtual AccountEntity Customer { get; private set; } = null!;
    public LeadSources? Source { get; private set; }
    public double? EstimatedRevenue { get; private set; }
    public string? Description { get; private set; }
    public LeadStatuses? Status { get; private set; }
    public LeadDisqualificationReasons? DisqualificationReason { get; private set; }
    public string? DisqualificationDescription { get; private set; }
    public virtual Deal? Deal { get; private set; }
}