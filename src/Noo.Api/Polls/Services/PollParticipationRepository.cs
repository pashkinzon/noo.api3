using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Polls.Models;

namespace Noo.Api.Polls.Services;

public class PollParticipationRepository : Repository<PollParticipationModel>, IPollParticipationRepository
{
    public Task<bool> ParticipationExistsAsync(Ulid pollId, Ulid? userId, string? userExternalIdentifier)
    {
        return Context.Set<PollParticipationModel>()
            .Where(p => p.PollId == pollId &&
                        (p.UserId == userId || p.UserExternalIdentifier == userExternalIdentifier))
            .AnyAsync();
    }
}

public static class IUnitOfWorkPollParticipationRepositoryExtension
{
    public static IPollParticipationRepository PollParticipationRepository(this IUnitOfWork unitOfWork)
    {
        return new PollParticipationRepository()
        {
            Context = unitOfWork.Context,
        };
    }
}
