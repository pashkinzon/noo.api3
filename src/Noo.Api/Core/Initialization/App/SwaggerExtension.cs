using Scalar.AspNetCore;
using Noo.Api.Core.Config.Env;
using Noo.Api.Core.Initialization.Configuration;

namespace Noo.Api.Core.Initialization.App;

public static class SwaggerExtension
{
    public static void UseNooSwagger(this WebApplication app, IConfiguration configuration)
    {
        var appConfig = configuration.GetSection(AppConfig.SectionName).GetOrThrow<AppConfig>();

        var swaggerConfig = configuration.GetSection(SwaggerConfig.SectionName).GetOrThrow<SwaggerConfig>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(options =>
            {
                options.RouteTemplate = "/openapi/{documentName}.json";
            });

            app.MapScalarApiReference(swaggerConfig.RoutePrefix, options =>
            {
                options.Title = swaggerConfig.Title;
            });
        }
    }
}
