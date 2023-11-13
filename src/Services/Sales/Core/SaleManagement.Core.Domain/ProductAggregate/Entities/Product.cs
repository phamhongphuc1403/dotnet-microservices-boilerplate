using BuildingBlock.Core.Domain;
using SaleManagement.Core.Domain.ProductAggregate.Entities.Enums;

namespace SaleManagement.Core.Domain.ProductAggregate.Entities;

public sealed class Product : AggregateRoot
{
    public Product(Guid id, string code, string name, double price, bool isAvailable, ProductType type,
        DateTime createdAt, string createdBy) : this(code,
        name, price, isAvailable, type)
    {
        Id = id;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
    }

    public Product(string code, string name, double price, bool isAvailable, ProductType type) : this()
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