using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Noo.Api.Calendar.DTO;
using Noo.Api.Calendar.Services;
using Noo.Api.Core.Request;
using Noo.Api.Core.Response;
using Noo.Api.Core.Utils.Versioning;
using ProducesAttribute = Noo.Api.Core.Documentation.ProducesAttribute;

namespace Noo.Api.Calendar;

[ApiVersion(NooApiVersions.Current)]
[ApiController]
[Route("calendar")]
public class CalendarController : ApiController
{
    private readonly ICalendarService _calendarService;

    public CalendarController(ICalendarService calendarService)
    {
        _calendarService = calendarService;
    }

    /// <summary>
    /// Gets the current user's calendar events.
    /// </summary>
    [HttpGet("{userId}")]
    [Authorize(Policy = CalendarPolicies.CanGetCalendarEvents)]
    [Produces(
        typeof(ApiResponseDTO<IEnumerable<CalendarEventDTO>>), StatusCodes.Status200OK,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> GetCalendarEventsAsync([FromRoute] Ulid userId)
    {
        var result = await _calendarService.GetCalendarEventsAsync(userId);

        return OkResponse(result);
    }
}
