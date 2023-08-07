using TinyCRM.Domain.Enums;

namespace TinyCRM.Domain.Entities
{
    public class LeadEntity : GuidBaseEntity
    {
        public string Title { get; set; } = null!;
        public Guid CustomerId { get; set; }
        public virtual AccountEntity Customer { get; set; } = null!;
        public LeadSources? Source { get; set; }
        public double? EstimatedRevenue { get; set; }
        public string? Description { get; set; }
        public LeadStatuses? Status { get; set; }
        public LeadDisqualificationReasons? DisqualificationReason { get; set; }
        public string? DisqualificationDescription { get; set; }
        public virtual DealEntity? Deal { get; set; }
    }
}