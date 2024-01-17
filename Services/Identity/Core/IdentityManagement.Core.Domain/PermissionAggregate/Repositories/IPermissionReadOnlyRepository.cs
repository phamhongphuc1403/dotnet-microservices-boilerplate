using BuildingBlock.Core.Domain.Repositories;
using IdentityManagement.Core.Domain.PermissionAggregate.Entities;

namespace IdentityManagement.Core.Domain.PermissionAggregate.Repositories;

public interface IPermissionReadOnlyRepository : IReadOnlyRepository<Permission>
{
    Task<IEnumerable<string>> GetNamesByRoleNameAsync(string roleName);
}