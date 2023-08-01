using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Entities.Enums;

namespace TinyCRM.Infrastructure.Repositories.Interfaces
{
    public interface IDealRepository : IRepository<DealEntity>
    {
        Task<DealStatusEnum> GetDealStatusById(Guid id);
    }
}
