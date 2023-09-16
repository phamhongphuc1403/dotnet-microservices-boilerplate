namespace TinyCRM.Application.Common.Interfaces;

public interface ICacheService
{
    Task<T?> GetRecordAsync<T>(string id);

    Task<bool> SetRecordAsync<T>(string id, T data, TimeSpan expireTime);

    Task<bool> RemoveRecordAsync(string id);

    Task ClearAllDbsAsync();
}