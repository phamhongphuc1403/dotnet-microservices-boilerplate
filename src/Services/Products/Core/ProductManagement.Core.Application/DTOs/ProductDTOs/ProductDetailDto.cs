using ProductManagement.Core.Domain.ProductAggregate.Entities.Enums;

namespace ProductManagement.Core.Application.DTOs.ProductDTOs;

public class ProductDetailDto
{
    public Guid Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public double Price { get; set; }

    public ProductType Type { get; set; }

    public bool IsAvailable { get; set; }
}