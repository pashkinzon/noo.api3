using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction.Cache;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Sessions.Models;
using Noo.Api.Core.Security.Authorization;
using Noo.Api.Core.Utils.DI;

namespace Noo.Api.Sessions.Services;

[RegisterScoped(typeof(IOnlineService))]
public class OnlineService : IOnlineService
{
    private readonly ICacheRepository _cache;
    private readonly NooDbContext _db;
    private static readonly TimeSpan _onlineTtl = SessionConfig.OnlineTtl;
    private static readonly TimeSpan _activeTtl = SessionConfig.ActiveTtl;

    public OnlineService(ICacheRepository cache, NooDbContext db)
    {
        _cache = cache;
        _db = db;
    }

    private static string SessionKey(Ulid sessionId) => $"online:session:{sessionId}";
    private static string UserKey(Ulid userId) => $"online:user:{userId}";
    private static string ActiveUserKey(Ulid userId) => $"active:user:{userId}";
    private static string OnlineUsersPattern => "online:user:*";

    public Task<DateTime?> GetLastOnlineBySessionAsync(Ulid sessionId)
        => _cache.GetAsync<DateTime?>(SessionKey(sessionId));

    public Task<DateTime?> GetLastOnlineByUserAsync(Ulid userId)
        => _cache.GetAsync<DateTime?>(UserKey(userId));

    public Task<int> GetOnlineCountAsync(UserRoles? role = null)
        => _cache.CountAsync(OnlineUsersPattern);

    public Task SetSessionOnlineAsync(Ulid sessionId)
    {
        var now = DateTime.UtcNow;
        return _cache.SetAsync(SessionKey(sessionId), now, _onlineTtl);
    }

    public Task SetUserOnlineAsync(Ulid userId)
    {
        var now = DateTime.UtcNow;
        // Two keys: short-lived for "online", long-lived for "active"
        // Fire-and-forget second set to avoid extra await, but keep simple with two awaits
        return Task.WhenAll(
            _cache.SetAsync(UserKey(userId), now, _onlineTtl),
            _cache.SetAsync(ActiveUserKey(userId), now, _activeTtl)
        );
    }

    public async Task<bool> IsUserOnlineAsync(Ulid userId)
    {
        var last = await GetLastOnlineByUserAsync(userId);
        return last.HasValue && DateTime.UtcNow - last.Value < _onlineTtl;
    }

    public async Task<IEnumerable<Ulid>> GetActiveUserIdsAsync()
    {
        var cutoff = DateTime.UtcNow.AddDays(-14);
        // Distinct users with any session active in last 14 days
        return await _db.Set<SessionModel>()
            .Where(s => (s.LastRequestAt ?? s.UpdatedAt ?? s.CreatedAt) >= cutoff)
            .Select(s => s.UserId)
            .Distinct()
            .ToListAsync();
    }
}
