using BuildingBlock.Core;
using BuildingBlock.EntityFrameworkCore;
using TinyCRM.SaleManagement.Domain.Entities;
using TinyCRM.SaleManagement.Domain.Repositories;

namespace TinyCRM.SaleManagement.EntityFrameworkCore.Repositories;

public class LeadReadOnlyRepository : ReadOnlyRepository<Lead>, ILeadReadOnlyRepository
{
    public LeadReadOnlyRepository(DbFactory dbFactory) : base(dbFactory)
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