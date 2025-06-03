using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.Request;
using Noo.Api.Core.Response;
using Noo.Api.Core.Utils.Versioning;
using Noo.Api.Polls.DTO;
using Noo.Api.Polls.Models;
using Noo.Api.Polls.Services;
using SystemTextJsonPatch;
using ProducesAttribute = Noo.Api.Core.Documentation.ProducesAttribute;

namespace Noo.Api.Polls;

[ApiVersion(NooApiVersions.Current)]
[ApiController]
[Route("poll")]
public class PollController : ApiController
{
    private readonly IPollService _pollService;

    public PollController(IPollService pollService)
    {
        _pollService = pollService;
    }

    /// <summary>
    /// Get a list of polls based on the provided criteria.
    /// </summary>
    [HttpGet]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = PollPolicies.CanGetPolls)]
    [Produces(
        typeof(ApiResponseDTO<IEnumerable<PollDTO>>), StatusCodes.Status200OK,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> GetPollsAsync([FromQuery] Criteria<PollModel> criteria)
    {
        var result = await _pollService.GetPollsAsync(criteria);
        return OkResponse(result);
    }

    /// <summary>
    /// Get a specific poll by its ID.
    /// </summary>
    [HttpGet("{id}")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = PollPolicies.CanGetPoll)]
    [Produces(
        typeof(ApiResponseDTO<PollDTO>), StatusCodes.Status200OK,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden,
        StatusCodes.Status404NotFound
    )]
    public async Task<IActionResult> GetPollAsync([FromRoute] Ulid id)
    {
        var result = await _pollService.GetPollAsync(id);
        return OkResponse(result);
    }

    /// <summary>
    /// Create a new poll.
    /// </summary>
    [HttpPost]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = PollPolicies.CanCreatePoll)]
    [Produces(
        typeof(ApiResponseDTO<IdResponseDTO>), StatusCodes.Status201Created,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> CreatePollAsync([FromBody] CreatePollDTO poll)
    {
        var id = await _pollService.CreatePollAsync(poll);
        return CreatedResponse(id);
    }

    /// <summary>
    /// Update an existing poll.
    /// </summary>
    [HttpPatch("{id}")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = PollPolicies.CanUpdatePoll)]
    [Produces(
        null, StatusCodes.Status204NoContent,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden,
        StatusCodes.Status404NotFound
    )]
    public async Task<IActionResult> UpdatePollAsync([FromRoute] Ulid pollId, [FromBody] JsonPatchDocument<UpdatePollDTO> poll)
    {
        await _pollService.UpdatePollAsync(pollId, poll);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = PollPolicies.CanDeletePoll)]
    [Produces(
        null, StatusCodes.Status204NoContent,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> DeletePollAsync([FromRoute] Ulid id)
    {
        await _pollService.DeletePollAsync(id);
        return NoContent();
    }

    // TODO: Implement voting functionality
}
