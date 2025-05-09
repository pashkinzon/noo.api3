using Noo.Api.Core.Config.Env;
using Noo.Api.Core.Initialization.Configuration;

namespace Noo.Api.Core.Initialization.ServiceCollection;

public static class ResponseCacheExtension
{
    public static void AddNooResponseCaching(this IServiceCollection services, IConfiguration configuration)
    {
        var httpConfig = configuration.GetSection(HttpConfig.SectionName).GetOrThrow<HttpConfig>();

        services.AddResponseCaching(options =>
        {
            options.UseCaseSensitivePaths = false;
            options.SizeLimit = httpConfig.CacheSizeLimit;
            options.MaximumBodySize = httpConfig.MaximumCacheBodySize;
        });
    }
}
