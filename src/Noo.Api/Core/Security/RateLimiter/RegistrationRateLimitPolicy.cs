using System.Threading.RateLimiting;

namespace Noo.Api.Core.Security.RateLimiter;

public class RegistrationRateLimitPolicy : INamedRateLimitPolicy
{
    public string PolicyName => "RegistrationPolicy";

    public Func<HttpContext, RateLimitPartition<string>> Partitioner => context =>
    {
        var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        return RateLimitPartition.GetFixedWindowLimiter(ip, _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 10,
            Window = TimeSpan.FromHours(1)
        });
    };
}
