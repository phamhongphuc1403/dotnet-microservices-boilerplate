using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Enums;

namespace TinyCRM.Application.Modules.Deal.DTOs
{
    public class GetAllDealsDTO : GuidBaseEntity
    {
        public string Title { get; set; } = null!;
        public string Customer { get; set; } = null!;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(DealStatusEnum))]
        public DealStatusEnum Status { get; set; }
    }
}