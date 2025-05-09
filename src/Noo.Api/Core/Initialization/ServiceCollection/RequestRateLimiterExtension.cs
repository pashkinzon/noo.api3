using System.Threading.RateLimiting;
using Noo.Api.Core.Security.RateLimiter;

namespace Noo.Api.Core.Initialization.ServiceCollection;

public static class RequestRateLimiterExtension
{
    public static void AddRequestRateLimiter(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            var globalPolicy = new GlobalRateLimitPolicy();
            options.GlobalLimiter = PartitionedRateLimiter.Create(globalPolicy.Partitioner);

            var loginPolicy = new LoginRateLimitPolicy();
            options.AddPolicy(loginPolicy.PolicyName, loginPolicy.Partitioner);

            var registrationPolicy = new RegistrationRateLimitPolicy();
            options.AddPolicy(registrationPolicy.PolicyName, registrationPolicy.Partitioner);
        });
    }
}
