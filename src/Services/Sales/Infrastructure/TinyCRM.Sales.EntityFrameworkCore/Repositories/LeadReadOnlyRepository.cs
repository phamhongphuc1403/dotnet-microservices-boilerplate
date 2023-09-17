using BuildingBlock.Application;
using BuildingBlock.Domain;
using BuildingBlock.EntityFrameworkCore;
using TinyCRM.Sales.Domain.Entities;
using TinyCRM.Sales.Domain.Repositories;

namespace TinyCRM.Sales.EntityFrameworkCore.Repositories;

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