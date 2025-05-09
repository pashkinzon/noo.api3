using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Noo.Api.Core.ThirdPartyServices;

public interface IHealthCheckable
{
    public Task<HealthCheckResult> HealthCheckAsync();
}
