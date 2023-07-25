using System.Text.Json.Serialization;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Entities.Enums;

namespace TinyCRM.API.Modules.Deal.DTOs
{
    public class GetDealDto : GuidBaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Customer { get; set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DealStatusEnum Status { get; set; }

        public string OriginatingLead { get; set; } = string.Empty;
        public string? Description { get; set; }
        public double EstimatedRevenue { get; set; }
        public double ActualRevenue { get; set; }
    }
}