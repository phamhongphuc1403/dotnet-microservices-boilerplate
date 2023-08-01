using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Entities.Enums;

namespace TinyCRM.Domain.Repositories
{
    public interface IDealRepository : IRepository<DealEntity>
    {
        Task<DealStatusEnum> GetDealStatusById(Guid id);
    }
}