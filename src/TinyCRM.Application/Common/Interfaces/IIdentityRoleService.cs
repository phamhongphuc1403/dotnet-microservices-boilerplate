using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Common.Interfaces;

public interface IIdentityRoleService
{
    Task AddToRoleAsync(string userId, string role);

    Task AddToRolesAsync(string userId, IEnumerable<string> roles);

    Task<List<RoleEntity>> GetAllRoles();

    Task<IEnumerable<string>> GetRoleNamesByUserId(string userId);

    Task<RoleEntity> GetRoleById(string roleId);

    Task RemoveFromRole(string userId);

    Task<IEnumerable<PermissionEntity>> GetAllPermissionsByRoleName(string roleName);

    Task UpdateRolePermissionAsync(string roleName, List<PermissionEntity> permissions);
}