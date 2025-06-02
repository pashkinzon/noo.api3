using Noo.Api.Core.Security.Authorization;

namespace Noo.Api.Sessions.Services;

public interface IOnlineService
{
    public Task<int> GetOnlineCountAsync(UserRoles? role = null);
    public Task<DateTime?> GetLastOnlineBySessionAsync(Ulid sessionId);
    public Task<DateTime?> GetLastOnlineByUserAsync(Ulid userId);
    public Task SetSessionOnlineAsync(Ulid sessionId);
}
