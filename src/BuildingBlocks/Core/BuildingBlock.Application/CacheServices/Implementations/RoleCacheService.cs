using BuildingBlock.Application.CacheServices.Abstractions;

namespace BuildingBlock.Application.CacheServices.Implementations;

public class RoleCacheService : IRoleCacheService
{
    private readonly ICacheService _cacheService;

    public RoleCacheService(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public Task<IEnumerable<string>?> GetAsync(string userId)
    {
        return _cacheService.GetRecordAsync<IEnumerable<string>?>(userId);
    }

    public Task SetAsync(string userId, IEnumerable<string> roles)
    {
        return _cacheService.SetRecordAsync(userId, roles, TimeSpan.FromMinutes(30));
    }
}