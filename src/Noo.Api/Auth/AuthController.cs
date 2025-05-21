using Microsoft.AspNetCore.Mvc;
using Noo.Api.Auth.DTO;
using Noo.Api.Auth.Services;
using Noo.Api.Core.Exceptions;
using Noo.Api.Core.Request;
using Noo.Api.Core.Response;
using Noo.Api.Core.Utils.Versioning;

namespace Noo.Api.Auth;

/// <summary>
/// Controller responsible for handling authentication and user account related actions.
/// </summary>
/// <remarks>
/// Provides endpoints for user login, registration, password reset, and email change operations.
/// </remarks>
[ApiVersion(NooApiVersions.Current)]
[ApiController]
[Route("auth")]
public class AuthController : ApiController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    [MapToApiVersion(NooApiVersions.Current)]
    [ProducesResponseType(typeof(ApiResponseDTO<LoginResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDTO request)
    {
        var result = await _authService.LoginAsync(request);

        return OkResponse(result);
    }

    [HttpPost("register")]
    [MapToApiVersion(NooApiVersions.Current)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterDTO request)
    {
        await _authService.RegisterAsync(request);

        return NoContent();
    }

    [HttpPatch("request-password-change")]
    [MapToApiVersion(NooApiVersions.Current)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RequestPasswordChangeAsync([FromBody] RequestPasswordChangeDTO request)
    {
        await _authService.RequestPasswordResetAsync(request.Email);

        return NoContent();
    }

    [HttpPatch("confirm-password-change")]
    [MapToApiVersion(NooApiVersions.Current)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ConfirmPasswordChangeAsync([FromBody] ConfirmPasswordChangeDTO request)
    {
        await _authService.ConfirmPasswordResetAsync(request.Email, request.Token, request.NewPassword);

        return NoContent();
    }

    [HttpPatch("request-email-change")]
    [MapToApiVersion(NooApiVersions.Current)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RequestEmailChangeAsync([FromBody] RequestEmailChangeDTO request)
    {
        await _authService.RequestEmailChangeAsync(User.GetId(), request.NewEmail);

        return NoContent();
    }

    [HttpPatch("confirm-email-change")]
    [MapToApiVersion(NooApiVersions.Current)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> ConfirmEmailChangeAsync([FromBody] ConfirmEmailChangeDTO request)
    {
        await _authService.ConfirmEmailChangeAsync(User.GetId(), request.Token);

        return NoContent();
    }
}
