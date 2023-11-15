using IdentityManagement.Core.Domain.PermissionAggregate.Entities;
using IdentityManagement.Core.Domain.RoleAggregate.Entities;

namespace IdentityManagement.Core.Domain.PermissionAggregate.DomainServices.Abstractions;

public interface IPermissionDomainService
{
    Task CheckValidOnAddRoleAsync(Permission permission, Role role);
}