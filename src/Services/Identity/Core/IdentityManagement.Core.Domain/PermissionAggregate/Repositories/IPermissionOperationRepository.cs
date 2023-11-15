using IdentityManagement.Core.Domain.PermissionAggregate.Entities;
using IdentityManagement.Core.Domain.RoleAggregate.Entities;

namespace IdentityManagement.Core.Domain.PermissionAggregate.Repositories;

public interface IPermissionOperationRepository
{
    Task AddRoleAsync(Permission permission, Role role);
}