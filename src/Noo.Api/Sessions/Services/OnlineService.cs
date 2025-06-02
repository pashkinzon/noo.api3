using Noo.Api.Core.Security.Authorization;

namespace Noo.Api.Sessions.Services;

public class OnlineService : IOnlineService
{
    public Task<DateTime?> GetLastOnlineBySessionAsync(Ulid sessionId)
    {
        throw new NotImplementedException();
    }

    public Task<DateTime?> GetLastOnlineByUserAsync(Ulid userId)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetOnlineCountAsync(UserRoles? role = null)
    {
        throw new NotImplementedException();
    }

    public Task SetSessionOnlineAsync(Ulid sessionId)
    {
        throw new NotImplementedException();
    }
}
