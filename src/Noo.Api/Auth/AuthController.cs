using Microsoft.AspNetCore.Mvc;
using Noo.Api.Auth.DTO;
using Noo.Api.Auth.Services;
using Noo.Api.Core.Request;
using Noo.Api.Core.Utils.Versioning;

namespace Noo.Api.Auth;

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
    public async Task<IActionResult> LoginAsync([FromBody] LoginDTO request)
    {
        var result = await _authService.LoginAsync(request);

        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterDTO request)
    {
        await _authService.RegisterAsync(request);

        return CreatedAtAction(nameof(LoginAsync), null);
    }

    [HttpPatch("request-password-change")]
    public async Task<IActionResult> RequestPasswordChangeAsync([FromBody] RequestPasswordChangeDTO request)
    {
        await _authService.RequestPasswordResetAsync(request.Email);

        return NoContent();
    }

    [HttpPatch("confirm-password-change")]
    public async Task<IActionResult> ConfirmPasswordChangeAsync([FromBody] ConfirmPasswordChangeDTO request)
    {
        await _authService.ConfirmPasswordResetAsync(request.Email, request.Token, request.NewPassword);

        return NoContent();
    }

    [HttpPatch("request-email-change")]
    public async Task<IActionResult> RequestEmailChangeAsync([FromBody] RequestEmailChangeDTO request)
    {
        await _authService.RequestEmailChangeAsync(request.NewEmail);

        return NoContent();
    }

    [HttpPatch("confirm-email-change")]
    public async Task<IActionResult> ConfirmEmailChangeAsync([FromBody] ConfirmEmailChangeDTO request)
    {
        await _authService.ConfirmEmailChangeAsync(request.Token);

        return NoContent();
    }
}
