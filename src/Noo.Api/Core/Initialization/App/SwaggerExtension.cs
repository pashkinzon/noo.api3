using Noo.Api.Core.Config;
using Noo.Api.Core.Config.Env;
using Noo.Api.Core.Initialization.Configuration;

namespace Noo.Api.Core.Initialization.App;

public static class SwaggerExtension
{
    public static void UseNooSwagger(this IApplicationBuilder app, IConfiguration configuration)
    {
        var appConfig = configuration.GetSection(AppConfig.SectionName).GetOrThrow<AppConfig>();

        var swaggerConfig = configuration.GetSection(SwaggerConfig.SectionName).GetOrThrow<SwaggerConfig>();

        if (appConfig.Mode == ApplicationMode.Development)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(swaggerConfig.Endpoint, swaggerConfig.Title);
                options.RoutePrefix = swaggerConfig.RoutePrefix;
            });
        }
    }
}
