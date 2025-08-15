namespace Noo.Api.Core.Request;

public sealed class UlidRouteConstraint : IRouteConstraint
{
    public bool Match(HttpContext? _, IRouter? __, string routeKey,
                      RouteValueDictionary values, RouteDirection ___)
    {
        if (!values.TryGetValue(routeKey, out var raw) || raw is null)
            return false;

        return Ulid.TryParse(raw.ToString(), out var ulid) && ulid != Ulid.Empty;
    }
}
