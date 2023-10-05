namespace BuildingBlock.Application;

public interface IRoleCacheService
{
    Task<IEnumerable<string>?> GetAsync(string userId);
    Task SetAsync(string userId, IEnumerable<string> roles);
}