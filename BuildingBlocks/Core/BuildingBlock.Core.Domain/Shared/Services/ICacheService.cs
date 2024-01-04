using BuildingBlock.Core.Domain.Shared.Constants;

namespace BuildingBlock.Core.Domain.Shared.Services;

public interface ICacheService
{
    Task<T?> GetRecordAsync<T>(RecordKey key);

    Task<bool> SetRecordAsync<T>(RecordKey key, T data, TimeSpan expireTime);

    Task<bool> RemoveRecordAsync(RecordKey key);

    Task ClearAllDbsAsync();

    Task<T> GetOrSetRecordAsync<T>(RecordKey key, Func<Task<T>> asyncFunc, TimeSpan expireTime);
}