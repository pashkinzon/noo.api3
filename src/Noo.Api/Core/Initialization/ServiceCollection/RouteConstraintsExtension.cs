using Noo.Api.Core.Request;

namespace Noo.Api.Core.Initialization.ServiceCollection;

public static class RouteConstraintsExtension
{
    public static void AddRouteConstraints(this IServiceCollection services)
    {
        services.Configure<RouteOptions>(options =>
        {
            options.ConstraintMap.Add("ulid", typeof(UlidRouteConstraint));
        });
    }
}
