using Noo.Api.Polls.DTO;

namespace Noo.Api.Polls.Services;

public interface IPollService
{
    public Task<PollDTO> CreatePollAsync(CreatePollDTO createPollDto);

    public Task<PollDTO> UpdatePollAsync(Ulid id, UpdatePollDTO updatePollDto);

    public Task DeletePollAsync(Ulid id);

    public Task<PollDTO?> GetPollAsync(Ulid id);

    public Task<ICollection<PollDTO>> GetPollsAsync();
}
