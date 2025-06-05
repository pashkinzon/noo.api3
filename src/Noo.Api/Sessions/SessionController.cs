using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Noo.Api.Core.Request;
using Noo.Api.Core.Response;
using Noo.Api.Core.Utils.Versioning;
using Noo.Api.Sessions.DTO;
using Noo.Api.Sessions.Services;
using ProducesAttribute = Noo.Api.Core.Documentation.ProducesAttribute;

namespace Noo.Api.Sessions;

[ApiVersion(NooApiVersions.Current)]
[ApiController]
[Route("session")]
public class SessionController : ApiController
{
    private readonly ISessionService _sessionService;

    public SessionController(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    /// <summary>
    /// Gets the current user's list of sessions.
    /// </summary>
    [MapToApiVersion(NooApiVersions.Current)]
    [HttpGet]
    [Authorize(Policy = SessionPolicies.CanGetOwnSessions)]
    [Produces(
        typeof(ApiResponseDTO<IEnumerable<SessionDTO>>), StatusCodes.Status200OK,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> GetSessionsAsync()
    {
        var userId = User.GetId();
        var sessions = await _sessionService.GetSessionsAsync(userId);

        return OkResponse(sessions);
    }

    /// <summary>
    /// Deleted the current user session.
    /// Typically used to log out the user.
    /// </summary>
    [MapToApiVersion(NooApiVersions.Current)]
    [HttpDelete]
    [Authorize(Policy = SessionPolicies.CanDeleteOwnSessions)]
    [Produces(
        null, StatusCodes.Status204NoContent,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> DeleteSessionAsync()
    {
        var userId = User.GetId();
        var sessionId = User.GetSessionId();

        await _sessionService.DeleteSessionAsync(sessionId, userId);

        return NoContent();
    }

    /// <summary>
    /// Deletes a specific session by its ID.
    /// </summary>
    [MapToApiVersion(NooApiVersions.Current)]
    [HttpDelete("{sessionId}")]
    [Authorize(Policy = SessionPolicies.CanDeleteOwnSessions)]
    [Produces(
        null, StatusCodes.Status204NoContent,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> DeleteSessionAsync([FromRoute] Ulid sessionId)
    {
        var userId = User.GetId();
        await _sessionService.DeleteSessionAsync(sessionId, userId);

        return NoContent();
    }
}
