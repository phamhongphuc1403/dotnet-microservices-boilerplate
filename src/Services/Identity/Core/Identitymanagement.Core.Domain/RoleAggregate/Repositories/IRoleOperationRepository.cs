using Identitymanagement.Core.Domain.RoleAggregate.Entities;

namespace Identitymanagement.Core.Domain.RoleAggregate.Repositories;

public interface IRoleOperationRepository
{
    Task CreateAsync(Role role);

    Task UpdateAsync(Role role);
}