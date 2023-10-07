using TinyCRM.Products.Domain.ProductAggregate.Entities;
using TinyCRM.Products.Domain.ProductAggregate.Entities.Enums;

namespace TinyCRM.Products.Domain.ProductAggregate.DomainServices;

public interface IProductDomainService
{
    Task<Product> CreateAsync(string code, string name, double price, bool isAvailable, ProductType type);
    Task<Product> EditAsync(Guid id, string code, string name, double price, bool isAvailable, ProductType type);
    Task<IEnumerable<Product>> RemoveManyAsync(IEnumerable<Guid> ids);
}