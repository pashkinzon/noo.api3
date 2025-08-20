using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Sessions.Models;

namespace Noo.Api.Sessions.Background;

public class SessionCleanupWorker : BackgroundService
{
    private readonly IServiceProvider _services;
    private readonly TimeSpan _interval = SessionConfig.CleanupInterval;

    public SessionCleanupWorker(IServiceProvider services)
    {
        _services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _services.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<NooDbContext>();
                var cutoff = DateTime.UtcNow.AddDays(-SessionConfig.SessionRetentionDays);
                await db.Set<SessionModel>()
                    .Where(s => (s.LastRequestAt ?? s.UpdatedAt ?? s.CreatedAt) < cutoff)
                    .ExecuteDeleteAsync(stoppingToken);
            }
            catch
            {
                // ignore errors, retry later
            }

            try
            {
                await Task.Delay(_interval, stoppingToken);
            }
            catch (TaskCanceledException)
            {
                break;
            }
        }
    }
}
