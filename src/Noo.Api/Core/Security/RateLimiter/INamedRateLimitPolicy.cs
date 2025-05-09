using System.Threading.RateLimiting;

namespace Noo.Api.Core.Security.RateLimiter;

public interface INamedRateLimitPolicy
{
    public string PolicyName { get; }

    public Func<HttpContext, RateLimitPartition<string>> Partitioner { get; }
}
