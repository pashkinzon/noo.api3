using Noo.Api.Sessions.DTO;

namespace Noo.Api.Sessions.Services;

public interface ISessionService
{
    public Task<IEnumerable<SessionDTO>> GetSessionsAsync(Ulid userId);
    public Task<Ulid> CreateSessionIfNotExistsAsync(HttpContext context, Ulid userId);
    public Task DeleteAllSessionsAsync(Ulid userId);
    public Task DeleteSessionAsync(Ulid sessionId, Ulid userId);
}
