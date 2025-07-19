using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Noo.Api.Core.Request;
using Noo.Api.Core.Response;
using Noo.Api.Core.Utils.Versioning;
using Noo.Api.Statistics.DTO;
using Noo.Api.Statistics.Services;
using ProducesAttribute = Noo.Api.Core.Documentation.ProducesAttribute;

namespace Noo.Api.Statistics;

[ApiVersion(NooApiVersions.Current)]
[ApiController]
[Route("statistics")]
public class StatisticsController : ApiController
{
    private readonly IStatisticsService _statisticsService;

    public StatisticsController(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    /// <summary>
    /// Retrieves the current statistics of the platform.
    /// </summary>
    [MapToApiVersion(NooApiVersions.Current)]
    [HttpGet("platform")]
    [Authorize(Policy = StatisticsPolicies.CanGetPlatformStatistics)]
    [Produces(
        typeof(ApiResponseDTO<StatisticsDTO>), StatusCodes.Status200OK,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> GetPlatformStatisticsAsync(
        [FromQuery] DateTime? from = null,
        [FromQuery] DateTime? to = null
    )
    {
        var statistics = await _statisticsService.GetPlatformStatisticsAsync(from, to);

        return OkResponse(statistics);
    }

    /// <summary>
    /// Retrieves the statistics of a specific student.
    /// </summary>
    /// <remarks>
    /// It returns 404 only if the student does not exist or is not a student.
    /// </remarks>
    [MapToApiVersion(NooApiVersions.Current)]
    [HttpGet("student/{studentId}")]
    [Authorize(Policy = StatisticsPolicies.CanGetStudentStatistics)]
    [Produces(
        typeof(ApiResponseDTO<StatisticsDTO>), StatusCodes.Status200OK,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> GetStudentStatisticsAsync(
        [FromRoute] Ulid studentId,
        [FromQuery] DateTime? from = null,
        [FromQuery] DateTime? to = null
    )
    {
        var statistics = await _statisticsService.GetStudentStatisticsAsync(studentId, from, to);

        return OkResponse(statistics);
    }

    /// <summary>
    /// Retrieves the statistics of a specific mentor.
    /// </summary>
    /// <remarks>
    /// It returns 404 only if the mentor does not exist or is not a mentor.
    /// </remarks>
    [MapToApiVersion(NooApiVersions.Current)]
    [HttpGet("mentor/{mentorId}")]
    [Authorize(Policy = StatisticsPolicies.CanGetMentorStatistics)]
    [Produces(
        typeof(ApiResponseDTO<StatisticsDTO>), StatusCodes.Status200OK,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> GetMentorStatisticsAsync(
        [FromRoute] Ulid mentorId,
        [FromQuery] DateTime? from = null,
        [FromQuery] DateTime? to = null
    )
    {
        var statistics = await _statisticsService.GetMentorStatisticsAsync(mentorId, from, to);

        return OkResponse(statistics);
    }
}
