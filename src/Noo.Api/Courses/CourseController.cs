using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.Request;
using Noo.Api.Core.Response;
using Noo.Api.Core.Utils.Versioning;
using Noo.Api.Courses.DTO;
using Noo.Api.Courses.Models;
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
    [MapToApiVersion(NooApiVersions.Current)]
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
        var userRole = User.GetRole();
        var course = await _courseService.GetByIdAsync(id, false); // TODO: handle inactive parts

        return course is null ? NotFound() : OkResponse(course);
    }

    [HttpGet]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = CoursePolicies.CanSearchCourses)]
    [Produces(
        typeof(ApiResponseDTO<IEnumerable<CourseModel>>), StatusCodes.Status200OK,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> GetCoursesAsync([FromQuery] Criteria<CourseModel> criteria)
    {
        var (courses, total) = await _courseService.SearchAsync(criteria);

        return OkResponse((courses, total));
    }
}
