using IdentityManagement.Core.Domain.RoleAggregate.Entities;

namespace IdentityManagement.Core.Domain.RoleAggregate.Repositories;

public interface IRoleOperationRepository
{
    Task CreateAsync(Role role);

    Task UpdateAsync(Role role);
}