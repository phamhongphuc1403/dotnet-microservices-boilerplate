using BuildingBlock.Core.Domain.Exceptions;

namespace IdentityManagement.Core.Domain.PermissionAggregate.Exceptions;

public class RolePermissionConflictException : EntityConflictException
{
    public RolePermissionConflictException(string permissionName, string roleName) : base(
        $"Permission '{permissionName}' is already in role '{roleName}'")
    {
    }
}