using System.Collections.Concurrent;
using System.Text.Json;

namespace Noo.Api.Core.DataAbstraction.Cache;

// Lightweight in-memory cache for tests and local fallback
public class MemoryCacheRepository : ICacheRepository
{
    private class Entry
    {
        public required string Json { get; init; }
        public DateTime? ExpiresAt { get; init; }
        public bool IsExpired => ExpiresAt.HasValue && DateTime.UtcNow >= ExpiresAt.Value;
    }

    private readonly ConcurrentDictionary<string, Entry> _store = new(StringComparer.Ordinal);

    public Task<int> CountAsync(string pattern = "*")
    {
        // Very simple wildcard support: only prefix* patterns used in our code
        var count = 0;
        var isWildcard = pattern.EndsWith("*");
        var prefix = isWildcard ? pattern.TrimEnd('*') : pattern;

        foreach (var kv in _store)
        {
            if (kv.Value.IsExpired)
            {
                _store.TryRemove(kv.Key, out _);
                continue;
            }
            if ((isWildcard && kv.Key.StartsWith(prefix, StringComparison.Ordinal)) || kv.Key == pattern)
            {
                count++;
            }
        }
        return Task.FromResult(count);
    }

    public Task<T?> GetAsync<T>(string key)
    {
        if (_store.TryGetValue(key, out var entry))
        {
            if (entry.IsExpired)
            {
                _store.TryRemove(key, out _);
                return Task.FromResult(default(T));
            }
            return Task.FromResult(JsonSerializer.Deserialize<T>(entry.Json));
        }
        return Task.FromResult(default(T));
    }

    public Task RemoveAsync(string key)
    {
        _store.TryRemove(key, out _);
        return Task.CompletedTask;
    }

    public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        var json = JsonSerializer.Serialize(value);
        var expiresAt = expiration.HasValue ? DateTime.UtcNow.Add(expiration.Value) : (DateTime?)null;
        _store[key] = new Entry { Json = json, ExpiresAt = expiresAt };
        return Task.CompletedTask;
    }
}
