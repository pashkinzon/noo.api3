using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Noo.Api.Core.Request;
using Noo.Api.Core.Response;
using Noo.Api.Core.Utils.Versioning;
using Noo.Api.Courses.DTO;
using Noo.Api.Courses.Filters;
using Noo.Api.Courses.Models;
using Noo.Api.Courses.Services;
using ProducesAttribute = Noo.Api.Core.Documentation.ProducesAttribute;

namespace Noo.Api.Courses;

[ApiVersion(NooApiVersions.Current)]
[ApiController]
[Route("course")]
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
    /// Retrieves a course and its chapter/material tree by its unique identifier.
    /// </summary>
    [HttpGet("{courseId}")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = CoursePolicies.CanGetCourse)]
    [Produces(
        typeof(ApiResponseDTO<CourseDTO>), StatusCodes.Status200OK,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden,
        StatusCodes.Status404NotFound
    )]
    public async Task<IActionResult> GetCourseAsync([FromRoute] Ulid courseId)
    {
        var userId = User.GetId();

        if (await _courseMembershipService.HasAccessAsync(courseId, userId))
        {
            var course = await _courseService.GetByIdAsync(courseId, false);

            return course is null ? NotFound() : OkResponse(course);
        }

        return Forbid();
    }

    /// <summary>
    /// Search courses
    /// </summary>
    [HttpGet]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = CoursePolicies.CanSearchCourses)]
    [Produces(
        typeof(ApiResponseDTO<IEnumerable<CourseModel>>), StatusCodes.Status200OK,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> GetCoursesAsync([FromQuery] CourseFilter filter)
    {
        var result = await _courseService.SearchAsync(filter);

        return OkResponse(result);
    }
}
