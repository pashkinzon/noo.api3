namespace Noo.Api.Sessions.Middleware;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseSessionActivity(this IApplicationBuilder app)
        => app.UseMiddleware<SessionActivityMiddleware>();
}
