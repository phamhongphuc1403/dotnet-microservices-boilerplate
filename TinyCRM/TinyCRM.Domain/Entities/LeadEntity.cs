using TinyCRM.Domain.Entities.Enums;

namespace TinyCRM.Domain.Entities
{
    public class LeadEntity : GuidBaseEntity
    {
        public string Title { get; set; } = null!;
        public Guid CustomerId { get; set; }
        public virtual AccountEntity Customer { get; set; }
        public LeadSourceEnum? Source { get; set; }
        public double? EstimatedRevenue { get; set; }
        public string? Description { get; set; }
        public LeadStatusEnum? Status { get; set; }
        public LeadDisqualificationReasonEnum? DisqualificationReason { get; set; }
        public string? DisqualificationDescription { get; set; }
        public virtual DealEntity? Deal { get; set; }
    }
}