using BuildingBlock.Domain;
using TinyCRM.Sales.Domain.Entities.Enums;

namespace TinyCRM.Sales.Domain.Entities;

public class Lead : GuidEntity
{
    public string Title { get; private set; } = null!;
    public Guid AccountId { get; }
    public virtual Account Account { get; private set; } = null!;
    public LeadSource? Source { get; }
    public double? EstimatedRevenue { get; }
    public string? Description { get; }
    public LeadStatus? Status { get; }
    public LeadDisqualificationReason? DisqualificationReason { get; }
    public string? DisqualificationDescription { get; }
    public virtual Deal? Deal { get; }
}