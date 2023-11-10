using Identitymanagement.Core.Domain.PermissionAggregate.Entities;

namespace Identitymanagement.Core.Domain.PermissionAggregate.Repositories;

public interface IPermissionReadOnlyRepository
{
    Task<IEnumerable<string>> GetNamesByRoleNameAsync(string roleName);

    Task<IEnumerable<Permission>> GetAllByRoleNameAsync(string roleName);
}