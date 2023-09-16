using BuildingBlock.Core;
using BuildingBlock.Core.Utils;
using TinyCRM.ProductManagement.Domain.Entities.Enums;
using TinyCRM.ProductManagement.Domain.Exceptions;
using TinyCRM.ProductManagement.Domain.Repositories;

namespace TinyCRM.ProductManagement.Domain.Entities;

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