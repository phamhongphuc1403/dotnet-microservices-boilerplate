namespace BuildingBlock.Application.CacheServices.Abstractions;

public interface IRoleCacheService
{
    Task<IEnumerable<string>?> GetAsync(string userId);
    Task SetAsync(string userId, IEnumerable<string> roles);
}