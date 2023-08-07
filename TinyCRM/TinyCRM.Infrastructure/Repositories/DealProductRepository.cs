using TinyCRM.Application.Utilities;
using TinyCRM.Domain.DTOs;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Repositories;

namespace TinyCRM.Infrastructure.Repositories
{
    public class DealProductRepository : Repository<DealProductEntity>, IDealProductRepository
    {
        public DealProductRepository(DbFactory dbFactory) : base(dbFactory)
        {
        }

        public Task<(List<DealProductEntity>, int)> GetPagedProductsByDealAsync(DataQueryDto<DealProductEntity> query, Guid dealId)
        {
            return GetPaginationAsync(
                PaginationBuilder<DealProductEntity>
                    .Init(query)
                    .AddConstraint(entity => entity.DealId == dealId)
                    .JoinTable("Product")
                    .Build());
        }

        public Task<bool> CheckIfProductInDeal(Guid dealId, Guid productId)
        {
            return CheckIfExistAsync(entity => entity.DealId == dealId && entity.ProductId == productId);
        }

        public Task<DealProductEntity?> GetProductInDeal(Guid dealId, Guid id)
        {
            return GetAnyAsync(entity => entity.Id == id && entity.DealId == dealId);
        }
    }
}