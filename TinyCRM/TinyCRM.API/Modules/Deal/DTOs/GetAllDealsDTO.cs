using System.Text.Json.Serialization;
using TinyCRM.Domain.Entities.Enums;

namespace TinyCRM.API.Modules.Deal.DTOs
{
    public class GetAllDealsDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Customer { get; set; } = string.Empty;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DealStatusEnum Status { get; set; }
    }
}
