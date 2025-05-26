using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Noo.Api.Core.Request;
using Noo.Api.Core.Response;
using Noo.Api.Core.Utils.Versioning;
using Noo.Api.Courses.DTO;
using Noo.Api.Courses.Services;
using ProducesAttribute = Noo.Api.Core.Documentation.ProducesAttribute;

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

    /// <summary>
    /// Retrieves a course by its unique identifier.
    /// </summary>
    [HttpGet("{id}")]
    [ApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = CoursePolicies.CanGetCourse)]
    [Produces(
        typeof(ApiResponseDTO<CourseDTO>), StatusCodes.Status200OK,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden,
        StatusCodes.Status404NotFound
    )]
    public async Task<IActionResult> GetCourseAsync([FromRoute] Ulid id)
    {
        var course = await _courseService.GetByIdAsync(id);

        return course is null ? NotFound() : OkResponse(course);
    }
}
