using TinyCRM.Core;
using TinyCRM.Core.Repositories;
using TinyCRM.SaleManagement.Domain.Entities;

namespace TinyCRM.SaleManagement.Domain.Repositories;

public interface ILeadReadOnlyRepository : IReadOnlyRepository<Lead>
{
    Task<(List<Lead>, int)> GetPagedLeadsAsync(FilterAndPagingQuery<Lead> query);
}