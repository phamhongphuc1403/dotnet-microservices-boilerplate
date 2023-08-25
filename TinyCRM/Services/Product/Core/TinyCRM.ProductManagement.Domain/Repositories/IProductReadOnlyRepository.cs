using TinyCRM.Core;
using TinyCRM.Core.BaseRepositories;
using TinyCRM.ProductManagement.Domain.Entities;

namespace TinyCRM.ProductManagement.Domain.Repositories;

public interface IProductReadOnlyRepository : IReadOnlyRepository<Product>
{
    Task<(List<Product>, int)> GetPagedProductsAsync(FilterAndPagingQuery<Product> query);

    Task<bool> CheckIfStringIdExist(string stringId);

    Task<bool> CheckIfStringIdExist(string stringId, Guid productId);
}