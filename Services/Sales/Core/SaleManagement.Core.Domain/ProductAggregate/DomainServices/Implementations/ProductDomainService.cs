using BuildingBlock.Core.Domain.Repositories;
using BuildingBlock.Core.Domain.Shared.Utils;
using BuildingBlock.Core.Domain.Specifications.Implementations;
using SaleManagement.Core.Domain.ProductAggregate.DomainServices.Abstractions;
using SaleManagement.Core.Domain.ProductAggregate.Entities;
using SaleManagement.Core.Domain.ProductAggregate.Entities.Enums;
using SaleManagement.Core.Domain.ProductAggregate.Exceptions;
using SaleManagement.Core.Domain.ProductAggregate.Specifications;

namespace SaleManagement.Core.Domain.ProductAggregate.DomainServices.Implementations;

public class ProductDomainService : IProductDomainService
{
    private readonly IReadOnlyRepository<Product> _productReadOnlyRepository;

    public ProductDomainService(IReadOnlyRepository<Product> productReadOnlyRepository)
    {
        _productReadOnlyRepository = productReadOnlyRepository;
    }

    public async Task<Product> CreateAsync(Guid id, string code, string name, double price, bool isAvailable,
        ProductType type, DateTime createdAt, string createdBy)
    {
        await CheckValidOnCreateAsync(id, code);

        return new Product(id, code, name, price, isAvailable, type, createdAt, createdBy);
    }

    public async Task<Product> EditAsync(Guid id, string code, string name, double price, bool isAvailable,
        ProductType type, DateTime? updatedAt, string? updatedBy)
    {
        var product = await CheckValidOnEditAsync(id, code);

        product.Code = code;
        product.Name = name;
        product.Price = price;
        product.IsAvailable = isAvailable;
        product.Type = type;
        product.UpdatedAt = updatedAt;
        product.UpdatedBy = updatedBy;

        return product;
    }

    public async Task<Product> DeleteAsync(Guid id, DateTime? deletedAt, string? deletedBy)
    {
        var product = await CheckValidOnDeleteAsync(id);

        product.DeletedAt = deletedAt;
        product.DeletedBy = deletedBy;

        return product;
    }

    private Task<Product> CheckValidOnDeleteAsync(Guid id)
    {
        return GetOrThrowAsync(id);
    }

    private async Task<Product> CheckValidOnEditAsync(Guid id, string code)
    {
        var product = await GetOrThrowAsync(id);

        await ThrowIfExist(id, code);

        return product;
    }

    private async Task ThrowIfExist(Guid id, string code)
    {
        var productCodeSpecification = new ProductCodeExactMatchSpecification(code);

        var productIdNotEqualSpecification = new EntityIdNotEqualSpecification<Product>(id);

        var specification = productCodeSpecification.And(productIdNotEqualSpecification);

        Optional<bool>.Of(await _productReadOnlyRepository.CheckIfExistAsync(specification))
            .ThrowIfExist(new ProductConflictExceptionException(nameof(code), code));
    }


    private async Task<Product> GetOrThrowAsync(Guid id)
    {
        var productIdSpecification = new EntityIdSpecification<Product>(id);

        return Optional<Product>.Of(await _productReadOnlyRepository.GetAnyAsync(productIdSpecification))
            .ThrowIfNotExist(new ProductNotFoundException(id)).Get();
    }

    private async Task CheckValidOnCreateAsync(Guid id, string code)
    {
        await ThrowIfExistAsync(id);
        await ThrowIfExistAsync(code);
    }


    private async Task ThrowIfExistAsync(Guid id)
    {
        var productIdSpecification = new EntityIdSpecification<Product>(id);

        Optional<bool>.Of(await _productReadOnlyRepository.CheckIfExistAsync(productIdSpecification))
            .ThrowIfExist(new ProductConflictExceptionException(id));
    }

    private async Task ThrowIfExistAsync(string code)
    {
        var productCodeSpecification = new ProductCodeExactMatchSpecification(code);

        Optional<bool>.Of(await _productReadOnlyRepository.CheckIfExistAsync(productCodeSpecification))
            .ThrowIfExist(new ProductConflictExceptionException(nameof(code), code));
    }
}