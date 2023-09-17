using BuildingBlock.Core;
using BuildingBlock.EntityFrameworkCore;
using TinyCRM.SaleManagement.Domain.Entities;
using TinyCRM.SaleManagement.Domain.Entities.Enums;
using TinyCRM.SaleManagement.Domain.Repositories;

namespace TinyCRM.SaleManagement.EntityFrameworkCore.Repositories;

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