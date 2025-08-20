using System.ComponentModel.DataAnnotations;

namespace Noo.Api.Core.Config.Env;

public class CacheConfig : IConfig
{
    public static string SectionName => "Cache";

    [Required]
    public required string ConnectionString { get; init; }

    [Required]
    public required int DefaultCacheTime { get; init; }

    public TimeSpan DefaultCacheTimeSpan => TimeSpan.FromSeconds(DefaultCacheTime);

    [Required]
    public required string Prefix { get; init; }

    // Optional selector to allow in-memory fallback in tests or local dev
    public string Provider { get; init; } = "Redis"; // Redis | Memory
}
