using BuildingBlock.Core.Domain.Shared.Constants;
using BuildingBlock.Core.Domain.Shared.Services;
using IdentityManagement.Core.Domain.PermissionAggregate.Repositories;

namespace IdentityManagement.Infrastructure.EntityFrameworkCore.CachedRepositories.CachedPermissionRepositories;

public class CachedPermissionReadOnlyRepository : IPermissionReadOnlyRepository
{
    private readonly ICacheService _cacheService;
    private readonly IPermissionReadOnlyRepository _permissionReadOnlyRepository;

    public CachedPermissionReadOnlyRepository(IPermissionReadOnlyRepository permissionReadOnlyRepository,
        ICacheService cacheService)
    {
        _permissionReadOnlyRepository = permissionReadOnlyRepository;
        _cacheService = cacheService;
    }

    public async Task<IEnumerable<string>> GetNamesByRoleNameAsync(string roleName)
    {
        return await _cacheService.GetOrSetRecordAsync(CacheKeyRegistry.GetPermissionsByRoleNameKey(roleName),
            async () => await _permissionReadOnlyRepository.GetNamesByRoleNameAsync(roleName),
            TimeSpan.FromMinutes(30));
    }
}