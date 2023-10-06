using BuildingBlock.Domain;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Utils;
using TinyCRM.Products.Domain.ProductAggregate.Entities.Enums;
using TinyCRM.Products.Domain.ProductAggregate.Exceptions;
using TinyCRM.Products.Domain.ProductAggregate.Specifications;

namespace TinyCRM.Products.Domain.ProductAggregate.Entities;

public class Product : AggregateRoot
{
    public Product()
    {
        
    }
    private Product(string code, string name, double price, bool isAvailable, ProductType type)
    {
        Code = code;
        Name = name;
        Price = price;
        IsAvailable = isAvailable;
        Type = type;
    }

    public string Code { get; private set; } = null!;
    public string Name { get; private set; } = null!;
    public double Price { get; private set; }
    public bool IsAvailable { get; private set; }
    public ProductType Type { get; private set; }

    public static async Task<Product> CreateAsync(string code, string name, double price, bool isAvailable,
        ProductType type, IReadOnlyRepository<Product> productReadOnlyRepository)
    {
        var productCodeSpecification = new ProductCodeExactMatchSpecification(code);

        Optional<bool>.Of(await productReadOnlyRepository.CheckIfExistAsync(productCodeSpecification))
            .ThrowIfPresent(new ProductConflictExceptionException(nameof(code), code));

        return new Product(code, name, price, isAvailable, type);
    }
}