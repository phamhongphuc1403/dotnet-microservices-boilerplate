using Identitymanagement.Core.Domain.PermissionAggregate.Entities;
using Identitymanagement.Core.Domain.RoleAggregate.Entities;

namespace Identitymanagement.Core.Domain.PermissionAggregate.DomainServices.Abstractions;

public interface IPermissionDomainService
{
    Task CheckValidOnAddRoleAsync(Permission permission, Role role);
}