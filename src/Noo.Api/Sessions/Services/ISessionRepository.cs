using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Sessions.Models;

namespace Noo.Api.Sessions.Services;

public interface ISessionRepository : IRepository<SessionModel>
{
    public Task DeleteAllSessionsAsync(Ulid userId);
    public Task DeleteSessionAsync(Ulid sessionId, Ulid userId);
    public Task<IEnumerable<SessionModel>> GetManyOfUserAsync(Ulid userId);
}
