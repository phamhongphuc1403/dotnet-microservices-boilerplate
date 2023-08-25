using TinyCRM.Core;
using TinyCRM.ProductManagement.Domain.Entities.Enums;

namespace TinyCRM.ProductManagement.Application.DTOs;

public class GetProductDto : GuidBaseEntity
{
    public string StringId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public ProductTypes Type { get; set; }
    public double Price { get; set; }
}