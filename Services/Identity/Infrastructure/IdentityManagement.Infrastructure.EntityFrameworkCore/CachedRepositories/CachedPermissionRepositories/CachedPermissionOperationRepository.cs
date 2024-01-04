using BuildingBlock.Core.Domain.Shared.Constants;
using BuildingBlock.Core.Domain.Shared.Services;
using IdentityManagement.Core.Domain.PermissionAggregate.Entities;
using IdentityManagement.Core.Domain.PermissionAggregate.Repositories;
using IdentityManagement.Core.Domain.RoleAggregate.Entities;

namespace IdentityManagement.Infrastructure.EntityFrameworkCore.CachedRepositories.CachedPermissionRepositories;

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

        await _cacheService.RemoveRecordAsync(CacheKeyRegistry.GetPermissionsByRoleNameKey(role.Name));
    }
}