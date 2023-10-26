using TinyCRM.Identity.Domain.RoleAggregate.Entities;

namespace TinyCRM.Identity.Domain.RoleAggregate.Repositories;

public interface IRoleOperationRepository
{
    Task CreateAsync(Role role);

    Task UpdateAsync(Role role);
}