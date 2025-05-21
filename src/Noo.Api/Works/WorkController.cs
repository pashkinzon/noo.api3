using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.Request;
using Noo.Api.Core.Response;
using Noo.Api.Core.Utils.Versioning;
using Noo.Api.Works.DTO;
using Noo.Api.Works.Models;
using Noo.Api.Works.Services;
using SystemTextJsonPatch;

namespace Noo.Api.Works;

[ApiVersion(NooApiVersions.Current)]
[ApiController]
[Route("work")]
public class WorkController : ApiController
{
    private readonly IWorkService _workService;

    public WorkController(IWorkService workService)
    {
        _workService = workService;
    }

    [HttpGet]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = WorkPolicies.CanSearchWorks)]
    [ProducesResponseType(typeof(ApiResponseDTO<IEnumerable<WorkResponseDTO>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetWorksAsync([FromQuery] Criteria<WorkModel> criteria)
    {
        var (results, count) = await _workService.GetWorksAsync(criteria);
        return OkResponse((results, count));
    }

    [HttpGet("{id}")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = WorkPolicies.CanGetWork)]
    [ProducesResponseType(typeof(ApiResponseDTO<WorkResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetWorkAsync([FromRoute] Ulid id)
    {
        var work = await _workService.GetWorkAsync(id);

        if (work == null)
        {
            return NotFound();
        }

        return OkResponse(work);
    }

    [HttpPost]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = WorkPolicies.CanCreateWorks)]
    [ProducesResponseType(typeof(ApiResponseDTO<IdResponseDTO>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateWorkAsync([FromBody] CreateWorkDTO work)
    {
        var id = await _workService.CreateWorkAsync(work);
        return CreatedResponse(id);
    }

    [HttpPatch("{id}")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = WorkPolicies.CanEditWorks)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateWorkAsync([FromRoute] Ulid id, [FromBody] JsonPatchDocument<UpdateWorkDTO> work)
    {
        await _workService.UpdateWorkAsync(id, work, ModelState);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = WorkPolicies.CanDeleteWorks)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteWorkAsync([FromRoute] Ulid id)
    {
        await _workService.DeleteWorkAsync(id);
        return NoContent();
    }
}
