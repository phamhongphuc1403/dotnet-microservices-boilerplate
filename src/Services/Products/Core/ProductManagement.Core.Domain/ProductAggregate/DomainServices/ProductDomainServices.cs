using BuildingBlock.Core.Domain.Repositories;
using BuildingBlock.Core.Domain.Shared.Utils;
using BuildingBlock.Core.Domain.Specifications.Implementations;
using ProductManagement.Core.Domain.ProductAggregate.Entities;
using ProductManagement.Core.Domain.ProductAggregate.Entities.Enums;
using ProductManagement.Core.Domain.ProductAggregate.Exceptions;
using ProductManagement.Core.Domain.ProductAggregate.Specifications;

namespace ProductManagement.Core.Domain.ProductAggregate.DomainServices;

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
        product.UpdatedAt = null;
        product.UpdatedBy = null;

        return product;
    }

    public async Task<IEnumerable<Product>> DeleteManyAsync(IEnumerable<Guid> ids)
    {
        List<Product> products = new();

        foreach (var id in ids)
        {
            var product = await CheckValidOnDeleteAsync(id);

            products.Add(product);
        }

        return products;
    }

    private Task<Product> CheckValidOnDeleteAsync(Guid id)
    {
        return GetOrThrowAsync(id);
    }

    private async Task CheckValidOnCreateAsync(string code)
    {
        await ThrowIfExistAsync(code);
    }

    private async Task ThrowIfExistAsync(string code)
    {
        var productCodeSpecification = new ProductCodeExactMatchSpecification(code);

        Optional<bool>.Of(await _readOnlyRepository.CheckIfExistAsync(productCodeSpecification))
            .ThrowIfExist(new ProductConflictExceptionException(nameof(code), code));
    }

    private async Task<Product> CheckValidOnEditAsync(Guid id, string code)
    {
        var product = await GetOrThrowAsync(id);

        await ThrowIfExistAsync(id, code);

        return product;
    }

    private async Task ThrowIfExistAsync(Guid id, string code)
    {
        var productCodeSpecification = new ProductCodeExactMatchSpecification(code);

        var productIdNotEqualSpecification = new EntityIdNotEqualSpecification<Product>(id);

        var specification = productCodeSpecification.And(productIdNotEqualSpecification);

        Optional<bool>.Of(await _readOnlyRepository.CheckIfExistAsync(specification))
            .ThrowIfExist(new ProductConflictExceptionException(nameof(code), code));
    }

    private async Task<Product> GetOrThrowAsync(Guid id)
    {
        var productIdSpecification = new EntityIdSpecification<Product>(id);

        return Optional<Product>.Of(await _readOnlyRepository.GetAnyAsync(productIdSpecification))
            .ThrowIfNotExist(new ProductNotFoundException(id)).Get();
    }
}