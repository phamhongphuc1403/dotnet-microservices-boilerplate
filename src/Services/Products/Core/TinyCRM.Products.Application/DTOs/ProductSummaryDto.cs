using TinyCRM.Products.Domain.ProductAggregate.Entities.Enums;

namespace TinyCRM.Products.Application.DTOs;

public class ProductSummaryDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public double Price { get; set; }
    public ProductType Type { get; set; }
}