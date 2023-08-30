using TinyCRM.Core;
using TinyCRM.Core.Repositories;
using TinyCRM.ProductManagement.Domain.Entities;

namespace TinyCRM.ProductManagement.Domain.Repositories;

public interface IProductReadOnlyRepository : IReadOnlyRepository<Product>
{
    Task<(List<Product>, int)> GetPagedProductsAsync(FilterAndPagingQuery<Product> query);

    Task<bool> CheckIfCodeExist(string code);

    Task<bool> CheckIfCodeExist(string code, Guid productId);
}