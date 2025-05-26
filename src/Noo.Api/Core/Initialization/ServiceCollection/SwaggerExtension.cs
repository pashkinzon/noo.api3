using System.Reflection;
using Microsoft.OpenApi.Models;
using Noo.Api.Core.Config.Env;
using Noo.Api.Core.Documentation;
using Noo.Api.Core.Initialization.Configuration;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Noo.Api.Core.Initialization.ServiceCollection;

public static class AppSwaggerExtension
{
    public static void AddNooSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        var swaggerConfig = configuration.GetSection(SwaggerConfig.SectionName).GetOrThrow<SwaggerConfig>();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(swaggerConfig.Version, new OpenApiInfo
            {
                Title = swaggerConfig.Title,
                Description = swaggerConfig.Description,
                Version = swaggerConfig.Version,
            });

            options.AddJwtSecurityDefinition();
            options.OperationFilter<ProducesOperationFilter>();
            options.SchemaFilter<UlidSchemaFilter>();

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        });
    }

    private static void AddJwtSecurityDefinition(this SwaggerGenOptions options)
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer"
        });
    }
}
