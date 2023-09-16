using TinyCRM.ProductManagement.Domain.Entities.Enums;

namespace TinyCRM.ProductManagement.Application.DTOs;

public class CreateOrEditProductDto
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public ProductTypes Type { get; set; }
    public double Price { get; set; }
    public bool IsAvailable { get; set; }
}