using TinyCRM.Domain.Entities.Enums;
using TinyCRM.Domain.Entities;
using System.Text.Json.Serialization;

namespace TinyCRM.API.Modules.Lead.DTOs
{
    public class GetLeadDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
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
