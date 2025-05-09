using Microsoft.Extensions.Diagnostics.HealthChecks;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.System.HealthChecks;

namespace Noo.Api.Core.Initialization.ServiceCollection;

public static class HealthcheckExtension
{
    public static void AddHealthcheckServices(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddSystemChecks(tags: ["live"])
            .AddDbContextCheck(tags: ["ready"]);
    }

    private static IHealthChecksBuilder AddSystemChecks(this IHealthChecksBuilder builder, string[] tags)
    {
        // basic liveness probe
        builder.AddCheck("liveness", () => HealthCheckResult.Healthy(), tags: tags);

        // memory usage
        builder.AddCheck<MemoryHealthCheck>("memory", tags: tags);

        return builder;
    }

    private static IHealthChecksBuilder AddDbContextCheck(this IHealthChecksBuilder builder, string[] tags)
    {
        builder.AddCheck<DbContextHealthCheck>("database", tags: tags);

        return builder;
    }
}
