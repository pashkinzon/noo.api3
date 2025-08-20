using System.Text.Json;

namespace Noo.Api.Core.DataAbstraction.Cache;

public static class CacheRepositoryExtensions
{
    public static async Task<T?> GetOrSetAsync<T>(this ICacheRepository cache, string key, Func<Task<T>> factory, TimeSpan? expiration = null)
    {
        var existing = await cache.GetAsync<T>(key);
        if (!Equals(existing, default(T)))
        {
            return existing;
        }

        var created = await factory();
        await cache.SetAsync(key, created!, expiration);
        return created;
    }

    public static Task<T?> GetOrSetAsync<T>(this ICacheRepository cache, string prefix, object keyObject, Func<Task<T>> factory, TimeSpan? expiration = null)
    {
        var key = BuildKey(prefix, keyObject);
        return cache.GetOrSetAsync(key, factory, expiration);
    }

    public static string BuildKey(string prefix, object keyObject)
    {
        // Stable JSON string as part of the key. Keep small; truncate if huge.
        var json = JsonSerializer.Serialize(keyObject);
        if (json.Length > 512)
        {
            json = json.Substring(0, 512);
        }
        var baseKey = $"{prefix}:{json}";
        // Normalize whitespace
        return baseKey.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
    }
}
