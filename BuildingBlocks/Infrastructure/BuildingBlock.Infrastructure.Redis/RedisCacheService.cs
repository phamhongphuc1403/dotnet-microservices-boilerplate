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

    public async Task<T?> GetRecordAsync<T>(string id)
    {
        var stringData = await _db.StringGetAsync(NormalizeId(id));

        return string.IsNullOrWhiteSpace(stringData)
            ? default
            : JsonConvert.DeserializeObject<T>(stringData.ToString());
    }

    public Task<bool> SetRecordAsync<T>(string id, T data, TimeSpan expireTime)
    {
        var serializedData = JsonConvert.SerializeObject(data);

        return _db.StringSetAsync(NormalizeId(id), serializedData, expireTime, When.Always);
    }

    public Task<bool> RemoveRecordAsync(string id)
    {
        return _db.KeyDeleteAsync(NormalizeId(id));
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

    public async Task<T> GetOrSetRecordAsync<T>(string key, Func<Task<T>> asyncFunc, TimeSpan expireTime)
    {
        var cachedRecords = await GetRecordAsync<T?>(key);

        if (cachedRecords is not null) return cachedRecords;

        var dbRecords = await asyncFunc();

        await SetRecordAsync(key, dbRecords, expireTime);

        return dbRecords;
    }

    private static string NormalizeId(string id)
    {
        return id.ToUpper();
    }
}