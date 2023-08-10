using TinyCRM.Domain.DTOs;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Domain.Repositories;

public interface ILeadRepository : IRepository<LeadEntity>
{
    Task<(List<LeadEntity>, int)> GetPagedLeadsAsync(DataQueryDto<LeadEntity> query);

    Task<(List<LeadEntity>, int)> GetPagedLeadsByCustomerIdAsync(DataQueryDto<LeadEntity> query, Guid customerId);
}