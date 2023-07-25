using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Entities.Enums;

namespace TinyCRM.API.Modules.Deal.DTOs
{
    public class GetAllDealsDto : GuidBaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Customer { get; set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(DealStatusEnum))]
        public DealStatusEnum Status { get; set; }
    }
}