using TinyCRM.Application.Common.Interfaces;
using TinyCRM.Application.Modules.Permission.DTOs;
using TinyCRM.Application.Utilities;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.HttpExceptions;

namespace TinyCRM.Application.Modules.Permission.Services;

public class PermissionService : IPermissionService
{
    private readonly IIdentityRoleService _identityRoleService;

    public PermissionService(
        IIdentityRoleService identityRoleService)
    {
        _identityRoleService = identityRoleService;
    }

    public List<PermissionEntity> GetAll()
    {
        return Domain.Constants.Permission.PermissionsList.ToList();
    }

    public Task<IEnumerable<PermissionEntity>> GetAllByRoleNameAsync(string roleName)
    {
        return _identityRoleService.GetAllPermissionsByRoleName(roleName);
    }

    public async Task UpdateAsync(string roleName, UpdateRolePermissionsDto dto)
    {
        var permissions = new List<PermissionEntity>();
        foreach (var permissionType in dto.PermissionTypes)
        {
            var permission = Optional<PermissionEntity>
                .Of(Domain.Constants.Permission.PermissionsList.FirstOrDefault(permission =>
                    permission.Type == permissionType))
                .ThrowIfNotPresent(new NotFoundException("Permission not found")).Get();

            permissions.Add(permission);
        }

        await _identityRoleService.UpdateRolePermissionAsync(roleName, permissions);
    }
}