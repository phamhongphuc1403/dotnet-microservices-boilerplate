using BuildingBlock.Core.Domain;
using ProductManagement.Core.Domain.ProductAggregate.Entities.Enums;

namespace ProductManagement.Core.Domain.ProductAggregate.Entities;

public class Product : AggregateRoot
{
    public Product(string code, string name, double price, bool isAvailable, ProductType type)
    {
        Code = code;
        Name = name;
        Price = price;
        IsAvailable = isAvailable;
        Type = type;
    }

    public Product()
    {
    }

    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public double Price { get; set; }
    public bool IsAvailable { get; set; }
    public ProductType Type { get; set; }
}