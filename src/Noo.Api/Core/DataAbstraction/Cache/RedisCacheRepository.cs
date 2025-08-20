
using System.Text.Json;
using Microsoft.Extensions.Options;
using Noo.Api.Core.Config.Env;
using StackExchange.Redis;

namespace Noo.Api.Core.DataAbstraction.Cache;

public class RedisCacheRepository : ICacheRepository
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    private readonly IDatabase _database;
    private readonly CacheConfig _cacheConfig;

    public RedisCacheRepository(
        IConnectionMultiplexer connectionMultiplexer,
        IOptions<CacheConfig> cacheConfig
    )
    {
        _connectionMultiplexer = connectionMultiplexer;
        _database = _connectionMultiplexer.GetDatabase();
        _cacheConfig = cacheConfig.Value;
    }

    public async Task<int> CountAsync(string pattern = "*")
    {
        // Initialize total key count
        var totalCount = 0;
        // Get all endpoints in the Redis configuration
        var endpoints = _connectionMultiplexer.GetEndPoints();

        foreach (var endpoint in endpoints)
        {
            // Get server instance for each endpoint
            var server = _connectionMultiplexer.GetServer(endpoint);

            // Skip replica servers to avoid double-counting
            if (server.IsReplica) continue;

            // Iterate through keys matching pattern using SCAN (non-blocking)
            await foreach (var _ in server.KeysAsync(
                database: _database.Database,
                pattern: pattern,
                pageSize: 1000)) // Efficient batch size
            {
                totalCount++;
            }
            // TODO: implement a non-blocking key count
        }

        return totalCount;
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        // Retrieve value from Redis
        var redisValue = await _database.StringGetAsync(key);

        // Return default if key doesn't exist
        if (redisValue.IsNullOrEmpty)
            return default;

        // Deserialize JSON to requested type
        return JsonSerializer.Deserialize<T>(redisValue.ToString());
    }

    public async Task RemoveAsync(string key)
    {
        await _database.KeyDeleteAsync(key);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        string serializedValue = JsonSerializer.Serialize(value);

        // Set value in Redis with optional expiration
        await _database.StringSetAsync(
            key,
            serializedValue,
            expiry: expiration ?? _cacheConfig.DefaultCacheTimeSpan
        );
    }
}
