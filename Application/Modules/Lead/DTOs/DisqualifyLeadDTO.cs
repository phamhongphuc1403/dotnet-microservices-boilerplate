using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TinyCRM.Domain.Enums;

namespace TinyCRM.Application.Modules.Lead.DTOs
{
    public class DisqualifyLeadDTO
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(LeadDisqualificationReasonEnum))]
        public LeadDisqualificationReasonEnum? DisqualificationReason { get; set; }

        public string? DisqualificationDescription { get; set; }
    }
}