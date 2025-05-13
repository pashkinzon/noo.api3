using Microsoft.AspNetCore.Mvc;
using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.Request;
using Noo.Api.Subjects.DTO;
using Noo.Api.Subjects.Models;
using Noo.Api.Subjects.Services;
using SystemTextJsonPatch;

namespace Noo.Api.Subjects;

public class SubjectController : ApiController
{
    private readonly ISubjectService _subjectService;

    public SubjectController(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    [HttpGet]
    public async Task<IActionResult> GetSubjectsAsync([FromQuery] Criteria<SubjectModel> criteria)
    {
        var (items, total) = await _subjectService.GetSubjectsAsync(criteria);
        return Ok((items, total));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSubjectByIdAsync([FromRoute] Ulid id)
    {
        var subject = await _subjectService.GetSubjectByIdAsync(id);
        return subject is null ? NotFound() : Ok(subject);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubjectAsync([FromBody] SubjectCreationDTO subject)
    {
        var id = await _subjectService.CreateSubjectAsync(subject);
        return CreatedResponse(id);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateSubjectAsync([FromRoute] Ulid id, [FromBody] JsonPatchDocument<SubjectUpdateDTO> subject)
    {
        await _subjectService.UpdateSubjectAsync(id, subject);

        return NoContent();
    }
}
