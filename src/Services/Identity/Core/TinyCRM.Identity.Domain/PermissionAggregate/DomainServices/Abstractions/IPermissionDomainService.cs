using TinyCRM.Identity.Domain.PermissionAggregate.Entities;
using TinyCRM.Identity.Domain.RoleAggregate.Entities;

namespace TinyCRM.Identity.Domain.PermissionAggregate.DomainServices.Abstractions;

public interface IPermissionDomainService
{
    Task CheckValidOnAddRoleAsync(Permission permission, Role role);
}