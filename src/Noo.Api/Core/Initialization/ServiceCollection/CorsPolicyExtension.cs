using Noo.Api.Core.Config.Env;
using Noo.Api.Core.Initialization.Configuration;

namespace Noo.Api.Core.Initialization.ServiceCollection;

public static class CorsPolicyExtension
{
    public static void AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
    {
        var appConfig = configuration.GetSection(AppConfig.SectionName).GetOrThrow<AppConfig>();

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.WithOrigins(appConfig.AllowedOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
    }
}
