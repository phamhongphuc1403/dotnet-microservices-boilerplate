using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TinyCRM.Domain.Entities.Enums;

namespace TinyCRM.API.Modules.Lead.DTOs
{
    public class DisqualifyLeadDTO
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(LeadDisqualificationReasonEnum))]
        public LeadDisqualificationReasonEnum? DisqualificationReason { get; set; }
        public string? DisqualificationDescription { get; set; }
    }
}
