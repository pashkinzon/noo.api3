using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Sessions.Models;
using Microsoft.EntityFrameworkCore;

namespace Noo.Api.Sessions.Services;

public class SessionRepository : Repository<SessionModel>, ISessionRepository
{
    public Task DeleteAllSessionsAsync(Ulid userId)
    {
        var set = Context.GetDbSet<SessionModel>();
        var toRemove = set.Where(s => s.UserId == userId);
        set.RemoveRange(toRemove);
        return Task.CompletedTask;
    }

    public async Task DeleteSessionAsync(Ulid sessionId, Ulid userId)
    {
        var set = Context.GetDbSet<SessionModel>();
        var entity = await set.FirstOrDefaultAsync(s => s.Id == sessionId && s.UserId == userId);
        if (entity != null)
        {
            set.Remove(entity);
        }
    }

    public async Task<IEnumerable<SessionModel>> GetManyOfUserAsync(Ulid userId)
    {
        var set = Context.GetDbSet<SessionModel>();
        var nowOrdered = await set
            .Where(s => s.UserId == userId)
            .OrderByDescending(s => s.LastRequestAt ?? s.UpdatedAt ?? s.CreatedAt)
            .ToListAsync();
        return nowOrdered;
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
