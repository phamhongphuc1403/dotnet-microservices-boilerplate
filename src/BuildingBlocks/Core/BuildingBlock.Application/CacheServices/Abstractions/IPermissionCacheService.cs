namespace BuildingBlock.Application;

public interface IPermissionCacheService
{
    Task<IEnumerable<string>?> GetAsync(string role);
    Task SetAsync(string role, IEnumerable<string> permissions);
}