using BuildingBlock.Core.Domain.Shared.Services;
using IdentityManagement.Core.Domain.PermissionAggregate.Entities;
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
        return await _cacheService.GetOrSetRecordAsync(roleName,
            async () => await _permissionReadOnlyRepository.GetNamesByRoleNameAsync(roleName),
            TimeSpan.FromMinutes(30));
    }

    public Task<IEnumerable<Permission>> GetAllByRoleNameAsync(string roleName)
    {
        return _permissionReadOnlyRepository.GetAllByRoleNameAsync(roleName);
    }
}