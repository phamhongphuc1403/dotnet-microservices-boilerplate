using BuildingBlock.Domain;
using BuildingBlock.Domain.Utils;
using TinyCRM.Products.Domain.Entities.Enums;
using TinyCRM.Products.Domain.Exceptions;
using TinyCRM.Products.Domain.Repositories;

namespace TinyCRM.Products.Domain.Entities;

public class Product : GuidBaseEntity
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public double Price { get; private set; }
    public bool IsAvailable { get; private set; }
    public ProductTypes Type { get; private set; }

    private Product(string code, string name, double price, bool isAvailable, ProductTypes type)
    {
        Code = code;
        Name = name;
        Price = price;
        IsAvailable = isAvailable;
        Type = type;
    }
    
    public static async Task<Product> CreateAsync(string code, string name, double price, bool isAvailable, ProductTypes type, IProductReadOnlyRepository productReadOnlyRepository)
    {
        Optional<bool>.Of(await productReadOnlyRepository.CheckIfCodeExist(code))
            .ThrowIfPresent(new ProductDuplicateException(code));

        return new Product(code, name, price, isAvailable, type);
    }
}