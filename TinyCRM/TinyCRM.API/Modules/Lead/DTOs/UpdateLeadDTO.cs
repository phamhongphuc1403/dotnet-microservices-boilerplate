using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TinyCRM.Domain.Entities.Enums;

namespace TinyCRM.API.Modules.Lead.DTOs
{
    public class UpdateLeadDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public Guid CustomerId { get; set; }

        public string? Description { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(LeadSourceEnum))]
        public LeadSourceEnum? Source { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(LeadStatusEnum))]
        public LeadStatusEnum Status { get; set; }

        public double? EstimatedRevenue { get; set; }
    }
}