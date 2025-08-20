using Noo.Api.Core.Config.Env;
using Noo.Api.Core.DataAbstraction.Cache;
using Noo.Api.Core.Initialization.Configuration;
using StackExchange.Redis;

namespace Noo.Api.Core.Initialization.ServiceCollection;

public static class CacheProviderExtension
{
    public static void AddCacheProvider(this IServiceCollection services, IConfiguration configuration)
    {
        var cacheConfig = configuration.GetSection(CacheConfig.SectionName).GetOrThrow<CacheConfig>();

        var useMemory = string.Equals(cacheConfig.Provider, "Memory", StringComparison.OrdinalIgnoreCase);

        if (!useMemory)
        {
            // Redis distributed cache and connection multiplexer
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = cacheConfig.ConnectionString;
                options.InstanceName = cacheConfig.Prefix;
            });

            services.AddSingleton<IConnectionMultiplexer>(_ =>
                ConnectionMultiplexer.Connect(cacheConfig.ConnectionString)
            );

            services.AddScoped<ICacheRepository, RedisCacheRepository>();
        }
        else
        {
            // In-memory fallback for tests/dev
            services.AddDistributedMemoryCache();
            services.AddSingleton<ICacheRepository, MemoryCacheRepository>();
        }
    }
}
