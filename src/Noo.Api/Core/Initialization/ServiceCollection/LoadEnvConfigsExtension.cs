using Noo.Api.Core.Config;
using Noo.Api.Core.Config.Env;

namespace Noo.Api.Core.Initialization.ServiceCollection;

public static class LoadEnvConfigsExtension
{
    public static void LoadEnvConfigs(this IServiceCollection services, IConfiguration configuration)
    {
        AddConfig<AppConfig>(services, configuration, AppConfig.SectionName);
        AddConfig<DbConfig>(services, configuration, DbConfig.SectionName);
        AddConfig<CacheConfig>(services, configuration, CacheConfig.SectionName);
        AddConfig<JwtConfig>(services, configuration, JwtConfig.SectionName);
        AddConfig<SwaggerConfig>(services, configuration, SwaggerConfig.SectionName);
        AddConfig<LogConfig>(services, configuration, LogConfig.SectionName);
    }

    private static void AddConfig<TConfig>(IServiceCollection services, IConfiguration config, string sectionName) where TConfig : class, IConfig
    {
        services.AddOptions<TConfig>()
            .Bind(config.GetSection(sectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}
