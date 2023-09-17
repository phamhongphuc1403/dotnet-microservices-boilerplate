using BuildingBlock.Core;
using BuildingBlock.EntityFrameworkCore;
using TinyCRM.SaleManagement.Domain.Entities;
using TinyCRM.SaleManagement.Domain.Repositories;

namespace TinyCRM.SaleManagement.EntityFrameworkCore.Repositories;

public class LeadReadOnlyRepository : ReadOnlyRepository<SaleDbContext, Lead>, ILeadReadOnlyRepository
{
    public LeadReadOnlyRepository(SaleDbContext dbContext) : base(dbContext)
    {
    }

    public Task<(List<Lead>, int)> GetPagedLeadsAsync(FilterAndPagingQuery<Lead> query)
    {
        return GetFilterAndPagingAsync(
            FilterAndPagingBuilder<Lead>
                .Init(query)
                .Build());
    }
}