using TinyCRM.Domain.DTOs;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Domain.Repositories
{
    public interface IDealProductRepository : IRepository<DealProductEntity>
    {
        Task<(List<DealProductEntity>, int)> GetPagedProductsByDealAsync(DataQueryDto<DealProductEntity> query, Guid dealId);

        Task<bool> CheckIfProductInDeal(Guid dealId, Guid productId);

        Task<DealProductEntity?> GetProductInDeal(Guid dealId, Guid Id);
    }
}