﻿using BuildingBlock.Application.CacheServices.Abstractions;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace BuildingBlock.Redis;

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

        return _db.StringSetAsync(NormalizeId(id), serializedData, expireTime, When.NotExists);
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

    private static string NormalizeId(string id)
    {
        return id.ToUpper();
    }
}