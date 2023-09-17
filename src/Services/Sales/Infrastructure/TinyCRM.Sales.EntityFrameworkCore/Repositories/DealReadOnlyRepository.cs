using BuildingBlock.Application;
using BuildingBlock.Domain;
using BuildingBlock.EntityFrameworkCore;
using TinyCRM.Sales.Domain.Entities;
using TinyCRM.Sales.Domain.Entities.Enums;
using TinyCRM.Sales.Domain.Repositories;

namespace TinyCRM.Sales.EntityFrameworkCore.Repositories;

public class DealReadOnlyRepository : ReadOnlyRepository<SaleDbContext, Deal>, IDealReadOnlyRepository
{
    public DealReadOnlyRepository(SaleDbContext dbContext) : base(dbContext)
    {
    }
    
    public Task<bool> CheckIfOpenDealByIdExist(Guid id)
    {
        return CheckIfExistAsync(deal => deal.Id == id && deal.Status == DealStatuses.Open);
    }

    public Task<(List<Deal>, int)> GetPagedDealsAsync(FilterAndPagingQuery<Deal> query)
    {
        return GetFilterAndPagingAsync(
            FilterAndPagingBuilder<Deal>
                .Init(query)
                .Build());
    }
}