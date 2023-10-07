using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Specifications.Implementations;
using BuildingBlock.Domain.Utils;
using TinyCRM.Products.Domain.ProductAggregate.Entities;
using TinyCRM.Products.Domain.ProductAggregate.Entities.Enums;
using TinyCRM.Products.Domain.ProductAggregate.Exceptions;
using TinyCRM.Products.Domain.ProductAggregate.Specifications;

namespace TinyCRM.Products.Domain.ProductAggregate.DomainServices;

public class ProductDomainService : IProductDomainService
{
    private readonly IReadOnlyRepository<Product> _readOnlyRepository;

    public ProductDomainService(IReadOnlyRepository<Product> readOnlyRepository)
    {
        _readOnlyRepository = readOnlyRepository;
    }

    public async Task<Product> CreateAsync(string code, string name, double price, bool isAvailable, ProductType type)
    {
        await CheckValidOnCreateAsync(code);

        return new Product(code, name, price, isAvailable, type);
    }

    public async Task<Product> EditAsync(Guid id, string code, string name, double price, bool isAvailable,
        ProductType type)
    {
        var product = await CheckValidOnEditAsync(id, code);

        product.Code = code;
        product.Name = name;
        product.Price = price;
        product.IsAvailable = isAvailable;
        product.Type = type;

        return product;
    }

    public async Task<IEnumerable<Product>> RemoveManyAsync(IEnumerable<Guid> ids)
    {
        List<Product> products = new();

        foreach (var id in ids)
        {
            var productIdSpecification = new EntityIdSpecification<Product>(id);

            var product = Optional<Product>.Of(await _readOnlyRepository.GetAnyAsync(productIdSpecification))
                .ThrowIfNotPresent(new ProductNotFoundException(id)).Get();

            products.Add(product);
        }

        return products;
    }

    private async Task CheckValidOnCreateAsync(string code)
    {
        var productCodeSpecification = new ProductCodeExactMatchSpecification(code);

        Optional<bool>.Of(await _readOnlyRepository.CheckIfExistAsync(productCodeSpecification))
            .ThrowIfPresent(new ProductConflictExceptionException(nameof(code), code));
    }

    private async Task<Product> CheckValidOnEditAsync(Guid id, string code)
    {
        var product = await CheckIdExistOnEditAsync(id);

        await CheckCodeExistOnEditAsync(id, code);

        return product;
    }

    private async Task CheckCodeExistOnEditAsync(Guid id, string code)
    {
        var productCodeSpecification = new ProductCodeExactMatchSpecification(code);

        var productIdNotEqualSpecification = new EntityIdNotEqualSpecification<Product>(id);

        var specification = productCodeSpecification.And(productIdNotEqualSpecification);

        Optional<bool>.Of(await _readOnlyRepository.CheckIfExistAsync(specification))
            .ThrowIfPresent(new ProductConflictExceptionException(nameof(code), code));
    }

    private async Task<Product> CheckIdExistOnEditAsync(Guid id)
    {
        var productIdSpecification = new EntityIdSpecification<Product>(id);

        return Optional<Product>.Of(await _readOnlyRepository.GetAnyAsync(productIdSpecification))
            .ThrowIfNotPresent(new ProductNotFoundException(id)).Get();
    }
}