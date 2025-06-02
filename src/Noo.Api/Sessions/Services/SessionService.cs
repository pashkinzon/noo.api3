using Noo.Api.Core.Utils.DI;
using Noo.Api.Sessions.DTO;
using Noo.Api.Sessions.Utils;

namespace Noo.Api.Sessions.Services;

[RegisterScoped(typeof(ISessionService))]
public class SessionService : ISessionService
{
    public Task<Ulid> CreateSessionIfNotExistsAsync(HttpContext context, Ulid userId)
    {
        if (context is null || context.User is null)
        {
            throw new ArgumentNullException(nameof(context), "HttpContext or User cannot be null.");
        }

        var sessionModel = context.AsSessionModel(userId);


    }

    public Task DeleteAllSessionsAsync(Ulid userId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCurrentSessionAsync(Ulid userId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteSessionAsync(Ulid sessionId, Ulid userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<SessionDTO>> GetSessionsAsync(Ulid userId)
    {
        throw new NotImplementedException();
    }
}
