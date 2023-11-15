using IdentityManagement.Core.Domain.PermissionAggregate.Entities;

namespace IdentityManagement.Core.Domain.PermissionAggregate.Repositories;

public interface IPermissionReadOnlyRepository
{
    Task<IEnumerable<string>> GetNamesByRoleNameAsync(string roleName);

    Task<IEnumerable<Permission>> GetAllByRoleNameAsync(string roleName);
}