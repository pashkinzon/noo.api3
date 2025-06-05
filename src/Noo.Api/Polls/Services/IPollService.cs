using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Polls.DTO;
using Noo.Api.Polls.Models;
using SystemTextJsonPatch;

namespace Noo.Api.Polls.Services;

public interface IPollService
{
    public Task<Ulid> CreatePollAsync(CreatePollDTO createPollDto);

    public Task UpdatePollAsync(Ulid id, JsonPatchDocument<UpdatePollDTO> updatePollDto);

    public Task DeletePollAsync(Ulid id);

    public Task<PollDTO?> GetPollAsync(Ulid id);

    public Task<ICollection<PollDTO>> GetPollsAsync(Criteria<PollModel> criteria);

    /// <summary>
    /// Participate for a user in a poll.
    /// The `userId` parameter is nullable since there are polls that do not reuiqre registration
    /// </summary>
    public Task ParticipateAsync(Ulid pollId, Ulid? userId, PollParticipationDTO participationDto);
}
