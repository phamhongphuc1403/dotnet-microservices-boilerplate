using BuildingBlock.Core.Domain;
using SaleManagement.Core.Domain.AccountAggregate.Entities;
using SaleManagement.Core.Domain.DealAggregate.Entities;
using SaleManagement.Core.Domain.LeadAggregate.Entities.Enums;

namespace SaleManagement.Core.Domain.LeadAggregate.Entities;

public class Lead : AggregateRoot
{
    public string Title { get; private set; } = null!;
    public Guid AccountId { get; }
    public Account Account { get; private set; } = null!;
    public LeadSource? Source { get; }
    public double? EstimatedRevenue { get; }
    public string? Description { get; }
    public LeadStatus? Status { get; }
    public LeadDisqualificationReason? DisqualificationReason { get; }
    public string? DisqualificationDescription { get; }
    public virtual Deal? Deal { get; }
}