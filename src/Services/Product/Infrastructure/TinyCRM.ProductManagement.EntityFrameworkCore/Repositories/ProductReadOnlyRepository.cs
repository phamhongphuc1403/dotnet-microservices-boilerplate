using BuildingBlock.Core;
using BuildingBlock.EntityFrameworkCore;
using TinyCRM.ProductManagement.Domain.Entities;
using TinyCRM.ProductManagement.Domain.Repositories;

namespace TinyCRM.ProductManagement.EntityFrameworkCore.Repositories;

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

    public Task<bool> CheckIfCodeExist(string code)
    {
        return CheckIfExistAsync(entity => entity.Code == code);
    }

    public Task<bool> CheckIfCodeExist(string code, Guid productId)
    {
        return CheckIfExistAsync(entity => entity.Code == code && entity.Id != productId);
    }
}