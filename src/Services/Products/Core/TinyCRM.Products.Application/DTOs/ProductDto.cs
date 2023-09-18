using BuildingBlock.Domain;
using TinyCRM.Products.Domain.Entities.Enums;

namespace TinyCRM.Products.Application.DTOs;

public class ProductDto : GuidEntity
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public ProductType Type { get; set; }
    public double Price { get; set; }
    public bool IsAvailable { get; set; }
}