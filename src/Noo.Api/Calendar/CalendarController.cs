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
    [HttpGet("{userId}/{year}/{month}")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = CalendarPolicies.CanGetCalendarEvents)]
    [Produces(
        typeof(ApiResponseDTO<IEnumerable<CalendarEventDTO>>), StatusCodes.Status200OK,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> GetCalendarEventsAsync(
        [FromRoute] Ulid userId,
        [FromRoute] int year,
        [FromRoute] int month
    )
    {
        var result = await _calendarService.GetCalendarEventsAsync(userId, year, month);
        return OkResponse(result);
    }

    /// <summary>
    /// Creates a new calendar event for the current user.
    /// </summary>
    [HttpPost]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = CalendarPolicies.CanCreateCalendarEvent)]
    [Produces(
        typeof(ApiResponseDTO<IdResponseDTO>), StatusCodes.Status201Created,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> CreateCalendarEventAsync([FromBody] CreateCalendarEventDTO dto)
    {
        var eventId = await _calendarService.CreateCalendarEventAsync(dto);
        return CreatedResponse(eventId);
    }

    /// <summary>
    /// Deletes a calendar event by its ID.
    /// </summary>
    [HttpDelete("{id}")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = CalendarPolicies.CanDeleteCalendarEvent)]
    [Produces(
        null, StatusCodes.Status204NoContent,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> DeleteCalendarEventAsync([FromRoute] Ulid id)
    {
        var userId = User.GetId();
        await _calendarService.DeleteCalendarEventAsync(userId, id);
        return NoContent();
    }
}
