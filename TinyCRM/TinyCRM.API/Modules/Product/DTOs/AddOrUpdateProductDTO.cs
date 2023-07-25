using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TinyCRM.Domain.Entities.Enums;

namespace TinyCRM.API.Modules.Product.DTOs
{
    public class AddOrUpdateProductDto
    {
        [Required]
        public string StringId { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(ProductTypeEnum))]
        public ProductTypeEnum Type { get; set; }

        [Required]
        public double Price { get; set; }
    }
}