using TinyCRM.Core;
using TinyCRM.Core.Repositories;
using TinyCRM.SaleManagement.Domain.Entities;

namespace TinyCRM.SaleManagement.Domain.Repositories;

public interface IDealReadOnlyRepository : IReadOnlyRepository<Deal>
{
    Task<bool> CheckIfOpenDealByIdExist(Guid id);

    Task<(List<Deal>, int)> GetPagedDealsAsync(FilterAndPagingQuery<Deal> query);
}