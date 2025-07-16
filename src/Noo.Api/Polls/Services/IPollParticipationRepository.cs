using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Polls.Models;

namespace Noo.Api.Polls.Services;

public interface IPollParticipationRepository : IRepository<PollParticipationModel>
{
    public Task<bool> ParticipationExistsAsync(Ulid pollId, Ulid? userId, string? userExternalIdentifier);
}

