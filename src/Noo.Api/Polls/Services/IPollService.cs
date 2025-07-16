using Microsoft.AspNetCore.Mvc.ModelBinding;
using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Polls.DTO;
using Noo.Api.Polls.Models;
using SystemTextJsonPatch;

namespace Noo.Api.Polls.Services;

public interface IPollService
{
    public Task<Ulid> CreatePollAsync(CreatePollDTO createPollDto);
    public Task UpdatePollAsync(Ulid id, JsonPatchDocument<UpdatePollDTO> updatePollDto, ModelStateDictionary? modelState = null);
    public Task DeletePollAsync(Ulid id);
    public Task<PollModel?> GetPollAsync(Ulid id);
    public Task<SearchResult<PollModel>> GetPollsAsync(Criteria<PollModel> criteria);
    public Task ParticipateAsync(Ulid pollId, PollParticipationDTO participationDto);
    public Task<SearchResult<PollParticipationModel>> GetPollParticipationsAsync(Ulid pollId, Criteria<PollParticipationModel> criteria);
    public Task<PollParticipationModel?> GetPollParticipationAsync(Ulid participationId);
    public Task UpdatePollAnswerAsync(Ulid answerId, JsonPatchDocument<UpdatePollAnswerDTO> updateAnswerDto, ModelStateDictionary modelState);
}
