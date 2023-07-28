using System.Text.Json.Serialization;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Entities.Enums;

namespace TinyCRM.API.Modules.Lead.DTOs
{
    public class GetLeadDTO : GuidBaseEntity
    {
        public string Title { get; set; } = null!;
        public Guid CustomerId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LeadSourceEnum? Source { get; set; }

        public double? EstimatedRevenue { get; set; }
        public string? Description { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LeadStatusEnum? Status { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LeadDisqualificationReasonEnum? DisqualificationReason { get; set; }

        public string? DisqualificationDescription { get; set; }
    }
}