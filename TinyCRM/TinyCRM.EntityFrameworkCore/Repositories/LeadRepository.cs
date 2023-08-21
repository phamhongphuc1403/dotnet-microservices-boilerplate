using TinyCRM.Application.Utilities;
using TinyCRM.Domain.DTOs;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Repositories;

namespace TinyCRM.EntityFrameworkCore.Repositories;

public class LeadRepository : Repository<LeadEntity>, ILeadRepository
{
    public LeadRepository(DbFactory dbFactory) : base(dbFactory)
    {
    }

    public Task<(List<LeadEntity>, int)> GetPagedLeadsAsync(DataQueryDto<LeadEntity> query)
    {
        return GetPaginationAsync(
            PaginationBuilder<LeadEntity>
                .Init(query)
                .Build());
    }

    public Task<(List<LeadEntity>, int)> GetPagedLeadsByCustomerIdAsync(DataQueryDto<LeadEntity> query, Guid customerId)
    {
        return GetPaginationAsync(
            PaginationBuilder<LeadEntity>
                .Init(query)
                .AddConstraint(entity => entity.CustomerId == customerId)
                .Build());
    }
}