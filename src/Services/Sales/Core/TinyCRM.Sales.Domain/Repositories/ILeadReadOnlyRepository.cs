using BuildingBlock.Domain;
using BuildingBlock.Domain.Repositories;
using TinyCRM.Sales.Domain.Entities;

namespace TinyCRM.Sales.Domain.Repositories;

public interface ILeadReadOnlyRepository : IReadOnlyRepository<Lead>
{
    Task<(List<Lead>, int)> GetPagedLeadsAsync(FilterAndPagingQuery<Lead> query);
}