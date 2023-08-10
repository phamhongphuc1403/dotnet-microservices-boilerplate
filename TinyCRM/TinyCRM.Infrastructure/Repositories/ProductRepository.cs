using TinyCRM.Application.Utilities;
using TinyCRM.Domain.DTOs;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Repositories;

namespace TinyCRM.Infrastructure.Repositories;

public class ProductRepository : Repository<ProductEntity>, IProductRepository
{
    public ProductRepository(DbFactory dbFactory) : base(dbFactory)
    {
    }

    public Task<(List<ProductEntity>, int)> GetPagedProductsAsync(DataQueryDto<ProductEntity> query)
    {
        return GetPaginationAsync(
            PaginationBuilder<ProductEntity>
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