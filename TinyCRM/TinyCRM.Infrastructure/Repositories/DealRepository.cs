using Microsoft.EntityFrameworkCore;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Enums;
using TinyCRM.Domain.Repositories;

namespace TinyCRM.Infrastructure.Repositories
{
    public class DealRepository : Repository<DealEntity>, IDealRepository
    {
        public DealRepository(DbFactory dbFactory) : base(dbFactory)
        {
        }

        public Task<DealStatusEnum> GetDealStatusById(Guid id)
        {
            return DbSet.Where(d => d.Id == id).Select(d => d.Status).FirstOrDefaultAsync();
        }
    }
}