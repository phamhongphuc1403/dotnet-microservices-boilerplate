using TinyCRM.Core;
using TinyCRM.EntityFrameworkCore;
using TinyCRM.ProductManagement.Domain.Entities;
using TinyCRM.ProductManagement.Domain.Repositories;

namespace TinyCRM.ProductManagement.EntityFrameworkCore;

public class ProductReadOnlyRepository : ReadOnlyRepository<Product>, IProductReadOnlyRepository
{
    public ProductReadOnlyRepository(DbFactory dbFactory) : base(dbFactory)
    {
    }

    public Task<(List<Product>, int)> GetPagedProductsAsync(FilterAndPagingQuery<Product> query)
    {
        return GetFilterAndPagingAsync(
            FilterAndPagingBuilder<Product>
                .Init(query)
                .Build());
    }

    public Task<bool> CheckIfStringIdExist(string stringId)
    {
        return CheckIfExistAsync(entity => entity.StringId == stringId);
    }

    public Task<bool> CheckIfStringIdExist(string stringId, Guid productId)
    {
        return CheckIfExistAsync(entity => entity.StringId == stringId && entity.Id != productId);
    }
}