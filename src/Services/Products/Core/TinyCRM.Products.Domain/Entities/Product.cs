using BuildingBlock.Domain;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Utils;
using TinyCRM.Products.Domain.Entities.Enums;
using TinyCRM.Products.Domain.Exceptions;
using TinyCRM.Products.Domain.Specifications;

namespace TinyCRM.Products.Domain.Entities;

public class Product : GuidEntity
{
    private Product(string code, string name, double price, bool isAvailable, ProductType type)
    {
        Code = code;
        Name = name;
        Price = price;
        IsAvailable = isAvailable;
        Type = type;
    }

    public string Code { get; private set; }
    public string Name { get; private set; }
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