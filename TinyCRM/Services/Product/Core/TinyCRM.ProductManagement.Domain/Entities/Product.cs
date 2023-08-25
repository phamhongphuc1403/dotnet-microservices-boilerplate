using TinyCRM.Core;
using TinyCRM.ProductManagement.Domain.Entities.Enums;

namespace TinyCRM.ProductManagement.Domain.Entities;

public class Product : GuidBaseEntity
{
    public string StringId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public double Price { get; set; }
    public bool IsAvailable { get; set; }
    public ProductTypes Type { get; set; }
}