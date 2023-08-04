using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TinyCRM.Domain.Enums;

namespace TinyCRM.Application.Modules.Product.DTOs
{
    public class AddOrUpdateProductDTO
    {
        public string StringId { get; set; } = null!;
        public string Name { get; set; } = null!;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(ProductTypeEnum))]
        public ProductTypeEnum Type { get; set; }

        public double Price { get; set; }
    }
}