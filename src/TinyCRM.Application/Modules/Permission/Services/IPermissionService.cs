using TinyCRM.Application.Modules.Permission.DTOs;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Modules.Permission.Services;

public interface IPermissionService
{
    List<PermissionEntity> GetAll();

    Task<IEnumerable<PermissionEntity>> GetAllByRoleNameAsync(string roleName);

    Task UpdateAsync(string roleName, UpdateRolePermissionsDto model);
}