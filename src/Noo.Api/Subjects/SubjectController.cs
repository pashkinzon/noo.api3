using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.Exceptions;
using Noo.Api.Core.Request;
using Noo.Api.Core.Response;
using Noo.Api.Core.Utils.Versioning;
using Noo.Api.Subjects.DTO;
using Noo.Api.Subjects.Models;
using Noo.Api.Subjects.Services;
using SystemTextJsonPatch;

namespace Noo.Api.Subjects;

[ApiVersion(NooApiVersions.Current)]
[ApiController]
[Route("subject")]
public class SubjectController : ApiController
{
    private readonly ISubjectService _subjectService;

    public SubjectController(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    [HttpGet]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = SubjectPolicies.CanGetSubjects)]
    [ProducesResponseType(typeof(ApiResponseDTO<IEnumerable<SubjectDTO>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetSubjectsAsync([FromQuery] Criteria<SubjectModel> criteria)
    {
        var (items, total) = await _subjectService.GetSubjectsAsync(criteria);
        return OkResponse((items, total));
    }

    [HttpGet("{id}")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = SubjectPolicies.CanGetSubject)]
    [ProducesResponseType(typeof(ApiResponseDTO<SubjectDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSubjectByIdAsync([FromRoute] Ulid id)
    {
        var subject = await _subjectService.GetSubjectByIdAsync(id);
        return subject is null ? NotFound() : OkResponse(subject);
    }

    [HttpPost]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = SubjectPolicies.CanCreateSubject)]
    [ProducesResponseType(typeof(ApiResponseDTO<IdResponseDTO>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateSubjectAsync([FromBody] SubjectCreationDTO subject)
    {
        var id = await _subjectService.CreateSubjectAsync(subject);
        return CreatedResponse(id);
    }

    [HttpPatch("{id}")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = SubjectPolicies.CanPatchSubject)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UpdateSubjectAsync([FromRoute] Ulid id, [FromBody] JsonPatchDocument<SubjectUpdateDTO> subject)
    {
        await _subjectService.UpdateSubjectAsync(id, subject);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = SubjectPolicies.CanDeleteSubject)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteSubjectAsync([FromRoute] Ulid id)
    {
        await _subjectService.DeleteSubjectAsync(id);

        return NoContent();
    }
}
