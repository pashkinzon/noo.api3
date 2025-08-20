using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Security.Authorization;
using Noo.Api.Sessions.Models;
using Noo.Api.Sessions.Services;

namespace Noo.Api.Sessions.Middleware;

public class SessionActivityMiddleware
{
    private readonly RequestDelegate _next;
    private static readonly TimeSpan _dbUpdateThrottle = SessionConfig.DbUpdateThrottle;

    public SessionActivityMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IOnlineService onlineService, NooDbContext dbContext)
    {
        // Proceed quickly if unauthenticated
        if (context.User?.Identity?.IsAuthenticated == true)
        {
            var sessionId = context.User.GetSessionId();
            var userId = context.User.GetId();

            // Mark session and user online in cache (TTL based)
            await onlineService.SetSessionOnlineAsync(sessionId);
            await onlineService.SetUserOnlineAsync(userId);

            // Throttle DB updates: compare against stored LastRequestAt from DB without loading entity
            try
            {
                // Upsert LastRequestAt every ~5 minutes
                var now = DateTime.UtcNow;
                // Use ExecuteUpdateAsync to avoid fetching entity
                var updated = await dbContext.Set<SessionModel>()
                    .Where(s => s.Id == sessionId && (s.LastRequestAt == null || now - s.LastRequestAt >= _dbUpdateThrottle))
                    .ExecuteUpdateAsync(update => update
                        .SetProperty(s => s.LastRequestAt, now)
                        .SetProperty(s => s.UpdatedAt, now)
                    );
                // If no rows updated, it's within throttle window; ignore
            }
            catch
            {
                // best-effort: ignore failures
            }
        }

        await _next(context);
    }
}

//
