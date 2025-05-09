using System.Threading.RateLimiting;

namespace Noo.Api.Core.Security.RateLimiter;

public class GlobalRateLimitPolicy
{
    public Func<HttpContext, RateLimitPartition<string>> Partitioner => context =>
    {
        var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        return RateLimitPartition.GetFixedWindowLimiter(ip, _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 100,
            Window = TimeSpan.FromMinutes(1)
        });
    };
}
