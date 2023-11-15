using SaleManagement.Core.Domain.ProductAggregate.Entities;
using SaleManagement.Core.Domain.ProductAggregate.Entities.Enums;

namespace SaleManagement.Core.Domain.ProductAggregate.DomainServices.Abstractions;

public interface IProductDomainService
{
    Task<Product> CreateAsync(Guid id, string code, string name, double price, bool isAvailable, ProductType type,
        DateTime createdAt, string createdBy);

    Task<Product> EditAsync(Guid id, string code, string name, double price, bool isAvailable, ProductType type,
        DateTime? updatedAt, string? updatedBy);

    Task<Product> DeleteAsync(Guid id, DateTime? deletedAt, string? deletedBy);
}