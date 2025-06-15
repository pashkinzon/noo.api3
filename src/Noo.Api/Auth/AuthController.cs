using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Noo.Api.Auth.DTO;
using Noo.Api.Auth.Services;
using Noo.Api.Core.Exceptions;
using Noo.Api.Core.Request;
using Noo.Api.Core.Response;
using Noo.Api.Core.Utils.Versioning;
using ProducesAttribute = Noo.Api.Core.Documentation.ProducesAttribute;

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

    /// <summary>
    /// Logs in a user with the provided credentials.
    /// </summary>
    [HttpPost("login")]
    [MapToApiVersion(NooApiVersions.Current)]
    [AllowAnonymous]
    [Produces(
        typeof(ApiResponseDTO<LoginResponseDTO>), StatusCodes.Status200OK,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDTO request)
    {
        var result = await _authService.LoginAsync(request);

        return OkResponse(result);
    }

    /// <summary>
    /// Registers a new user with the provided details.
    /// During registration, an email verification token is sent to the user's email address.
    /// </summary>
    [HttpPost("register")]
    [MapToApiVersion(NooApiVersions.Current)]
    [AllowAnonymous]
    [Produces(
        null, StatusCodes.Status204NoContent,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status409Conflict
    )]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterDTO request)
    {
        await _authService.RegisterAsync(request);

        return NoContent();
    }

    /// <summary>
    /// Checks if the username is already taken.
    /// Returns true if the username is free, otherwise false
    /// </summary>
    [MapToApiVersion(NooApiVersions.Current)]
    [HttpGet("username-check/{username}")]
    [AllowAnonymous]
    [Produces(
        typeof(ApiResponseDTO<bool>), StatusCodes.Status200OK
    )]
    public async Task<IActionResult> CheckUsernameAsync(
        [FromRoute] string username
    )
    {
        bool isUsernameFree = await _authService.CheckUsernameAsync(username);

        return OkResponse(isUsernameFree);
    }

    /// <summary>
    /// Requests a password change by sending a reset token to the user's email address.
    /// </summary>
    [HttpPatch("request-password-change")]
    [MapToApiVersion(NooApiVersions.Current)]
    [AllowAnonymous]
    [Produces(
        null, StatusCodes.Status204NoContent,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized
    )]
    public async Task<IActionResult> RequestPasswordChangeAsync([FromBody] RequestPasswordChangeDTO request)
    {
        await _authService.RequestPasswordResetAsync(request.Email);

        return NoContent();
    }

    /// <summary>
    /// Confirms a password change by validating the reset token and setting a new password.
    /// </summary>
    [HttpPatch("confirm-password-change")]
    [MapToApiVersion(NooApiVersions.Current)]
    [AllowAnonymous]
    [Produces(
        null, StatusCodes.Status204NoContent,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized
    )]
    public async Task<IActionResult> ConfirmPasswordChangeAsync([FromBody] ConfirmPasswordChangeDTO request)
    {
        await _authService.ConfirmPasswordResetAsync(request.Token, request.NewPassword);

        return NoContent();
    }

    /// <summary>
    /// Confirms an email change by validating the confirmation token.
    /// This endpoint is used to finalize the email change process after the user has requested it.
    /// </summary>
    [HttpPatch("confirm-email-change")]
    [MapToApiVersion(NooApiVersions.Current)]
    [AllowAnonymous]
    [Produces(
        null, StatusCodes.Status204NoContent,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status409Conflict
    )]
    public async Task<IActionResult> ConfirmEmailChangeAsync([FromBody] ConfirmEmailChangeDTO request)
    {
        await _authService.ConfirmEmailChangeAsync(User.GetId(), request.Token);

        return NoContent();
    }
}
