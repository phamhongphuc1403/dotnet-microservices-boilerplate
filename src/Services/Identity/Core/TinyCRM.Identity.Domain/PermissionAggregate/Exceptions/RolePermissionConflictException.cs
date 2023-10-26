using BuildingBlock.Domain.Exceptions;

namespace TinyCRM.Identity.Domain.PermissionAggregate.Exceptions;

public class RolePermissionConflictException : EntityConflictException
{
    public RolePermissionConflictException(string permissionName, string roleName) : base(
        $"Permission '{permissionName}' is already in role '{roleName}'")
    {
    }
}