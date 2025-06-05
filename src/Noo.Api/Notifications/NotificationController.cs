using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.Documentation;
using Noo.Api.Core.Request;
using Noo.Api.Core.Response;
using Noo.Api.Core.Utils.Versioning;
using Noo.Api.Notifications.DTO;
using Noo.Api.Notifications.Models;
using Noo.Api.Notifications.Services;
using ProducesAttribute = Noo.Api.Core.Documentation.ProducesAttribute;

namespace Noo.Api.Notifications;

[ApiVersion(NooApiVersions.Current)]
[ApiController]
[Route("notification")]
public class NotificationController : ApiController
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    /// <summary>
    /// Gets the notifications for the user.
    /// </summary>
    [MapToApiVersion(NooApiVersions.Current)]
    [HttpGet]
    [Authorize(Policy = NotificationPolicies.CanViewNotifications)]
    [Produces(
        typeof(ApiResponseDTO<IEnumerable<NotificationDTO>>), StatusCodes.Status200OK,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> GetNotificationsAsync(
        [FromQuery] Criteria<NotificationModel> criteria
    )
    {
        var userId = User.GetId();
        var result = await _notificationService.GetNotificationsAsync(userId, criteria);
        return OkResponse(result);
    }

    /// <summary>
    /// Mark a notification as read
    /// </summary>
    [MapToApiVersion(NooApiVersions.Current)]
    [HttpPatch("{notificationId}/mark-read")]
    [Authorize(Policy = NotificationPolicies.CanReadNotifications)]
    [Produces(
        null, StatusCodes.Status204NoContent,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden,
        StatusCodes.Status404NotFound
    )]
    public async Task<IActionResult> MarkAsReadAsync(
        [FromRoute] Ulid notificationId
    )
    {
        var userId = User.GetId();
        await _notificationService.MarkAsReadAsync(userId, notificationId);
        return NoContent();
    }

    /// <summary>
    /// Bulk create notiifcations
    /// </summary>
    [MapToApiVersion(NooApiVersions.Current)]
    [HttpPost]
    [Authorize(Policy = NotificationPolicies.CanBulkCreateNotifications)]
    [Produces(
        null, StatusCodes.Status204NoContent,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> BulkCreateNotificationsAsync(
        [FromBody] BulkCreateNotificationsDTO options
    )
    {
        await _notificationService.BulkCreateNotificationsAsync(options);
        return NoContent();
    }

    /// <summary>
    /// Deletes a notification
    /// </summary>
    [MapToApiVersion(NooApiVersions.Current)]
    [HttpDelete("{notificationId}")]
    [Authorize(Policy = NotificationPolicies.CanDeleteNotifications)]
    [Produces(
        null, StatusCodes.Status204NoContent,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> DeleteNotificationAsync(
        [FromRoute] Ulid notificationId
    )
    {
        var userId = User.GetId();
        await _notificationService.DeleteNotificationAsync(notificationId, userId);
        return NoContent();
    }
}
