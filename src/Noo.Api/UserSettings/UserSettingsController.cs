using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Noo.Api.Core.Request;
using Noo.Api.Core.Response;
using Noo.Api.Core.Utils.Versioning;
using Noo.Api.UserSettings.DTO;
using Noo.Api.UserSettings.Services;
using ProducesAttribute = Noo.Api.Core.Documentation.ProducesAttribute;

namespace Noo.Api.UserSettings;

[ApiVersion(NooApiVersions.Current)]
[ApiController]
[Route("user-settings")]
public class UserSettingsController : ApiController
{
    private readonly IUserSettingsService _userSettingsService;

    public UserSettingsController(IUserSettingsService userSettingsService)
    {
        _userSettingsService = userSettingsService;
    }

    /// <summary>
    /// Get the current user settings.
    /// This includes theme, font size, and other user preferences.
    /// </summary>
    /// <remarks>
    /// This endpoint always returns settings and never null or NotFound.
    /// If the user has not set any settings, default values will be returned.
    /// </remarks>
    [MapToApiVersion(NooApiVersions.Current)]
    [HttpGet]
    [Authorize(Policy = UserSettingsPolicies.CanGetUserSettings)]
    [Produces(
        typeof(ApiResponseDTO<UserSettingsDTO>), StatusCodes.Status200OK,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> GetUserSettingsAsync()
    {
        var userId = User.GetId();
        var settings = await _userSettingsService.GetUserSettingsAsync(userId);
        return OkResponse(settings);
    }

    /// <summary>
    /// Update the current user settings.
    /// </summary>
    [MapToApiVersion(NooApiVersions.Current)]
    [HttpPatch]
    [Authorize(Policy = UserSettingsPolicies.CanPatchUserSettings)]
    [Produces(
        null, StatusCodes.Status204NoContent,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> PatchUserSettingsAsync([FromBody] UserSettingsUpdateDTO userSettings)
    {
        var userId = User.GetId();
        await _userSettingsService.UpdateUserSettingsAsync(userId, userSettings);
        return NoContent();
    }
}
