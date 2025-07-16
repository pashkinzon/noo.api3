using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Polls.Models;

namespace Noo.Api.Polls.Services;

public class PollParticipationRepository : Repository<PollParticipationModel>, IPollParticipationRepository
{
    public async Task<bool> ParticipationExistsAsync(Ulid pollId, Ulid? userId, string? userExternalIdentifier)
    {
        var participation = await Context.Set<PollParticipationModel>()
            .Where(p => p.PollId == pollId &&
                        (p.UserId == userId || p.UserExternalIdentifier == userExternalIdentifier))
            .FirstOrDefaultAsync();

        return participation != null;
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
