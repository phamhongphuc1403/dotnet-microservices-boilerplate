using BuildingBlock.Application;
using BuildingBlock.Domain;
using BuildingBlock.EntityFrameworkCore;
using TinyCRM.Products.Domain.Entities;
using TinyCRM.Products.Domain.Repositories;

namespace TinyCRM.Products.EntityFrameworkCore.Repositories;

public class ProductReadOnlyRepository : ReadOnlyRepository<ProductDbContext, Product>, IProductReadOnlyRepository
{
    public ProductReadOnlyRepository(ProductDbContext dbContext) : base(dbContext)
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