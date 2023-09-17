using BuildingBlock.Domain;
using BuildingBlock.Domain.Repositories;
using TinyCRM.Sales.Domain.Entities;

namespace TinyCRM.Sales.Domain.Repositories;

public interface IDealReadOnlyRepository : IReadOnlyRepository<Deal>
{
    Task<bool> CheckIfOpenDealByIdExist(Guid id);

    Task<(List<Deal>, int)> GetPagedDealsAsync(FilterAndPagingQuery<Deal> query);
}