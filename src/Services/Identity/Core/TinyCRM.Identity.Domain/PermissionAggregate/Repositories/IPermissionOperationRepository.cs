using TinyCRM.Identity.Domain.PermissionAggregate.Entities;
using TinyCRM.Identity.Domain.RoleAggregate.Entities;

namespace TinyCRM.Identity.Domain.PermissionAggregate.Repositories;

public interface IPermissionOperationRepository
{
    Task AddRoleAsync(Permission permission, Role role);
}