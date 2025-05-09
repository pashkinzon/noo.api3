using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Noo.Api.Core.DataAbstraction.Db;

public class DbContextHealthCheck : IHealthCheck
{
    private readonly NooDbContext _dbContext;

    public DbContextHealthCheck(NooDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            await _dbContext.Database.CanConnectAsync(cancellationToken);
            return HealthCheckResult.Healthy();
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(ex.Message, ex);
        }
    }
}
