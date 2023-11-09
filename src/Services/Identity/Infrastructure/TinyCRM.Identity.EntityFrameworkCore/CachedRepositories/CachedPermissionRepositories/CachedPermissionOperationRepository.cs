using BuildingBlock.Domain.Shared.Services;
using TinyCRM.Identity.Domain.PermissionAggregate.Entities;
using TinyCRM.Identity.Domain.PermissionAggregate.Repositories;
using TinyCRM.Identity.Domain.RoleAggregate.Entities;

namespace TinyCRM.Identity.EntityFrameworkCore.CachedRepositories.CachedPermissionRepositories;

public class CachedPermissionOperationRepository : IPermissionOperationRepository
{
    private readonly ICacheService _cacheService;
    private readonly IPermissionOperationRepository _permissionOperationRepository;

    public CachedPermissionOperationRepository(IPermissionOperationRepository permissionOperationRepository,
        ICacheService cacheService)
    {
        _permissionOperationRepository = permissionOperationRepository;
        _cacheService = cacheService;
    }

    public async Task AddRoleAsync(Permission permission, Role role)
    {
        await _permissionOperationRepository.AddRoleAsync(permission, role);

        await _cacheService.RemoveRecordAsync(role.Name);
    }
}