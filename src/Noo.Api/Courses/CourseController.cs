using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Noo.Api.Core.Exceptions;
using Noo.Api.Core.Request;
using Noo.Api.Core.Utils.Versioning;
using Noo.Api.Courses.DTO;
using Noo.Api.Courses.Services;

namespace Noo.Api.Courses;

[ApiController]
[Route("course")]
[ApiVersion(NooApiVersions.Current)]
public class CourseController : ApiController
{
    private readonly ICourseService _courseService;

    private readonly ICourseMembershipService _courseMembershipService;

    public CourseController(
        ICourseService courseService,
        ICourseMembershipService courseMembershipService
    )
    {
        _courseService = courseService;
        _courseMembershipService = courseMembershipService;
    }

    [HttpGet("{id}")]
    [ApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = CoursePolicies.CanGetCourse)]
    [ProducesResponseType(typeof(CourseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCourseAsync([FromRoute] Ulid id)
    {
        var course = await _courseService.GetByIdAsync(id);

        return course is null ? NotFound() : OkResponse(course);
    }
}
