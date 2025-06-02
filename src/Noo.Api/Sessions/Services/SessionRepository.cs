using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Sessions.Models;

namespace Noo.Api.Sessions.Services;

public class SessionRepository : Repository<SessionModel>, ISessionRepository
{
    public Task DeleteAllSessionsAsync(Ulid userId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteSessionAsync(Ulid sessionId, Ulid userId)
    {
        throw new NotImplementedException();
    }
}


public static class IUnitOfWorkSessionRepositoryExtensions
{
    public static ISessionRepository SessionRepository(this IUnitOfWork unitOfWork)
    {
        return new SessionRepository
        {
            Context = unitOfWork.Context,
        };
    }
}
