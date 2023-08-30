using TinyCRM.Core;
using TinyCRM.EntityFrameworkCore;
using TinyCRM.SaleManagement.Domain.Entities;
using TinyCRM.SaleManagement.Domain.Entities.Enums;
using TinyCRM.SaleManagement.Domain.Repositories;

namespace TinyCRM.SaleManagement.EntityFrameworkCore.Repositories;

public class DealReadOnlyRepository : ReadOnlyRepository<Deal>, IDealReadOnlyRepository
{
    public DealReadOnlyRepository(DbFactory dbFactory) : base(dbFactory)
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