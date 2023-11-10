namespace BuildingBlock.Core.Domain.Shared.Services;

public interface ICacheService
{
    Task<T?> GetRecordAsync<T>(string id);

    Task<bool> SetRecordAsync<T>(string id, T data, TimeSpan expireTime);

    Task<bool> RemoveRecordAsync(string id);

    Task ClearAllDbsAsync();

    Task<T> GetOrSetRecordAsync<T>(string key, Func<Task<T>> asyncFunc, TimeSpan expireTime);
}