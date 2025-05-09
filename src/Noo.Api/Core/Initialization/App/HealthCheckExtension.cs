using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Noo.Api.Core.Initialization.App;

public static class HealthCheckExtension
{
    public static IApplicationBuilder MapHealthAllChecks(this WebApplication app)
    {
        app.UseHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = WriteResponseAsync,
        });

        return app;
    }

    private static Task WriteResponseAsync(HttpContext context, HealthReport result)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = result.Status == HealthStatus.Healthy
            ? StatusCodes.Status200OK
            : StatusCodes.Status503ServiceUnavailable;

        var json = JsonSerializer.Serialize(new
        {
            status = result.Status.ToString(),
            checks = result.Entries.Select(e => new
            {
                key = e.Key,
                value = Enum.GetName(typeof(HealthStatus), e.Value.Status),
                description = e.Value.Description,
                data = e.Value.Data
            })
        });

        return context.Response.WriteAsync(json);
    }
}
