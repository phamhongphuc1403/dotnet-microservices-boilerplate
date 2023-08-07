using TinyCRM.Domain.DTOs;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Domain.Repositories
{
    public interface IProductRepository : IRepository<ProductEntity>
    {
        Task<(List<ProductEntity>, int)> GetPagedProductsAsync(DataQueryDto<ProductEntity> query);

        Task<bool> CheckIfStringIdExist(string stringId);

        Task<bool> CheckIfStringIdExist(string stringId, Guid productId);
    }
}