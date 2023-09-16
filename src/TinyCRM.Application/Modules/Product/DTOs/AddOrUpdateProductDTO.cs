using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TinyCRM.Domain.Enums;

namespace TinyCRM.Application.Modules.Product.DTOs;

public class AddOrUpdateProductDto
{
    public string StringId { get; set; } = null!;
    public string Name { get; set; } = null!;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    [EnumDataType(typeof(ProductTypes))]
    public ProductTypes Type { get; set; }

    public double Price { get; set; }
}