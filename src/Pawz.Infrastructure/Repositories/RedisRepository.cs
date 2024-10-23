using Pawz.Domain.Interfaces;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Repositories;

public class RedisRepository : IRedisRepository
{
    private readonly IDatabase _database;
    private readonly IConnectionMultiplexer _redisConnection;

    public RedisRepository(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
        _redisConnection = redis;
    }

    public async Task<bool> SetDataAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        var serializedValue = JsonSerializer.Serialize(value);

        return await _database.StringSetAsync(key, serializedValue, expiry);
    }

    public async Task<T?> GetDataAsync<T>(string key)
    {
        var serializedValue = await _database.StringGetAsync(key);

        if (serializedValue.IsNullOrEmpty)
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(serializedValue);
    }

    public async Task<bool> DeleteDataAsync(string key)
    {
        return await _database.KeyDeleteAsync(key);
    }

    public async Task<bool> HasKeyAsync(string pattern)
    {
        var server = _redisConnection.GetServer(_redisConnection.GetEndPoints()[0]);

        var keys = server.Keys(pattern: pattern);

        return await Task.FromResult(keys.Any());
    }
}
