using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TinyCRM.Domain.Enums;

namespace TinyCRM.Application.Modules.Lead.DTOs
{
    public class DisqualifyLeadDto
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(LeadDisqualificationReasons))]
        public LeadDisqualificationReasons? DisqualificationReason { get; set; }

        public string? DisqualificationDescription { get; set; }
    }
}