using Identitymanagement.Core.Domain.PermissionAggregate.Entities;
using Identitymanagement.Core.Domain.RoleAggregate.Entities;

namespace Identitymanagement.Core.Domain.PermissionAggregate.Repositories;

public interface IPermissionOperationRepository
{
    Task AddRoleAsync(Permission permission, Role role);
}