using System.Text.Json.Serialization;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Entities.Enums;

namespace TinyCRM.API.Modules.Deal.DTOs
{
    public class GetDealDTO : GuidBaseEntity
    {
        public string Title { get; set; } = null!;
        public string Customer { get; set; } = null!;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DealStatusEnum Status { get; set; }

        public string OriginatingLead { get; set; } = null!;
        public string? Description { get; set; }
        public double EstimatedRevenue { get; set; }
        public double ActualRevenue { get; set; }
    }
}