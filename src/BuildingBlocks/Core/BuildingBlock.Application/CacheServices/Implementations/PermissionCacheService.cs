using BuildingBlock.Application.CacheServices.Abstractions;

namespace BuildingBlock.Application.CacheServices.Implementations;

public class PermissionCacheService : IPermissionCacheService
{
    private readonly ICacheService _cacheService;

    public PermissionCacheService(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public Task<IEnumerable<string>?> GetAsync(string role)
    {
        return _cacheService.GetRecordAsync<IEnumerable<string>?>(role);
    }

    public Task SetAsync(string role, IEnumerable<string> permissions)
    {
        return _cacheService.SetRecordAsync(role, permissions, TimeSpan.FromMinutes(30));
    }
}