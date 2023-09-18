using BuildingBlock.Domain;
using TinyCRM.Sales.Domain.Entities.Enums;

namespace TinyCRM.Sales.Domain.Entities;

public class Lead : GuidEntity
{
    public string Title { get; private set; } = null!;
    public Guid AccountId { get; private set; }
    public virtual Account Account { get; private set; } = null!;
    public LeadSource? Source { get; private set; }
    public double? EstimatedRevenue { get; private set; }
    public string? Description { get; private set; }
    public LeadStatus? Status { get; private set; }
    public LeadDisqualificationReason? DisqualificationReason { get; private set; }
    public string? DisqualificationDescription { get; private set; }
    public virtual Deal? Deal { get; private set; }
}