using TinyCRM.Domain.DTOs;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Domain.Repositories;

public interface IDealRepository : IRepository<DealEntity>
{
    Task<bool> CheckIfOpenDealByIdExist(Guid id);

    Task<(List<DealEntity>, int)> GetPagedDealsAsync(DataQueryDto<DealEntity> query);

    Task<(List<DealEntity>, int)> GetPagedDealsByCustomerIdAsync(DataQueryDto<DealEntity> query, Guid customerId);
}