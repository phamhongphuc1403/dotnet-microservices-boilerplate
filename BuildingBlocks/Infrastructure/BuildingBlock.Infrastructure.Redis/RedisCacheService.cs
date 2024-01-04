using BuildingBlock.Core.Domain.Shared.Constants;
using BuildingBlock.Core.Domain.Shared.Services;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace BuildingBlock.Infrastructure.Redis;

public class RedisCacheService : ICacheService
{
    private readonly IDatabase _db;
    private readonly IConnectionMultiplexer _redis;

    public RedisCacheService(IConnectionMultiplexer redis)
    {
        _redis = redis;
        _db = redis.GetDatabase();
    }

    public async Task<T?> GetRecordAsync<T>(RecordKey key)
    {
        var stringData = await _db.StringGetAsync(key.Value);

        return string.IsNullOrWhiteSpace(stringData)
            ? default
            : JsonConvert.DeserializeObject<T>(stringData.ToString());
    }

    public Task<bool> SetRecordAsync<T>(RecordKey key, T data, TimeSpan expireTime)
    {
        var serializedData = JsonConvert.SerializeObject(data);

        return _db.StringSetAsync(key.Value, serializedData, expireTime, When.Always);
    }

    public Task<bool> RemoveRecordAsync(RecordKey key)
    {
        return _db.KeyDeleteAsync(key.Value);
    }

    public async Task ClearAllDbsAsync()
    {
        var endpoints = _redis.GetEndPoints(true);

        foreach (var endpoint in endpoints)
        {
            var server = _redis.GetServer(endpoint);
            await server.FlushAllDatabasesAsync();
        }
    }

    public async Task<T> GetOrSetRecordAsync<T>(RecordKey key, Func<Task<T>> asyncFunc, TimeSpan expireTime)
    {
        var cachedRecords = await GetRecordAsync<T?>(key);

        if (cachedRecords is not null) return cachedRecords;

        var dbRecords = await asyncFunc();

        await SetRecordAsync(key, dbRecords, expireTime);

        return dbRecords;
    }
}