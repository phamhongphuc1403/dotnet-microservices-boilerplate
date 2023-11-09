using TinyCRM.Identity.Domain.PermissionAggregate.Entities;

namespace TinyCRM.Identity.Domain.PermissionAggregate.Repositories;

public interface IPermissionReadOnlyRepository
{
    Task<IEnumerable<string>> GetNamesByRoleNameAsync(string roleName);

    Task<IEnumerable<Permission>> GetAllByRoleNameAsync(string roleName);
}