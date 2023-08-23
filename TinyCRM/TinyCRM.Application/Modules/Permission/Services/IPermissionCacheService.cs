using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Modules.Permission.Services;

public interface IPermissionCacheService
{
    Task<IEnumerable<PermissionEntity>?> GetAllByRoleNameAsync(string roleName);
    
    Task<IEnumerable<PermissionEntity>> AddByRoleNameAsync(string roleName);

    Task<IEnumerable<PermissionEntity>> GetAllOrAddByRoleName(string roleName);
    
    Task RemoveByRoleNameAsync(string roleName);
}