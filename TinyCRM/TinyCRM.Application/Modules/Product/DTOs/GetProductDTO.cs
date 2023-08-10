using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Enums;

namespace TinyCRM.Application.Modules.Product.DTOs;

public class GetProductDto : GuidBaseEntity
{
    public string StringId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public ProductTypes Type { get; set; }
    public double Price { get; set; }
}