using TinyCRM.Application.Utilities;
using TinyCRM.Domain.DTOs;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Enums;
using TinyCRM.Domain.Repositories;

namespace TinyCRM.EntityFrameworkCore.Repositories;

public class DealRepository : Repository<DealEntity>, IDealRepository
{
    public DealRepository(DbFactory dbFactory) : base(dbFactory)
    {
    }

    public Task<bool> CheckIfOpenDealByIdExist(Guid id)
    {
        return CheckIfExistAsync(deal => deal.Id == id && deal.Status == DealStatuses.Open);
    }

    public Task<(List<DealEntity>, int)> GetPagedDealsAsync(DataQueryDto<DealEntity> query)
    {
        return GetPaginationAsync(
            PaginationBuilder<DealEntity>
                .Init(query)
                .JoinTable("Lead.Customer")
                .Build());
    }

    public Task<(List<DealEntity>, int)> GetPagedDealsByCustomerIdAsync(DataQueryDto<DealEntity> query, Guid customerId)
    {
        return GetPaginationAsync(PaginationBuilder<DealEntity>
            .Init(query)
            .AddConstraint(entity => entity.Lead.CustomerId == customerId)
            .JoinTable("Lead.Customer")
            .Build());
    }
}