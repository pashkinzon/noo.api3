using Microsoft.AspNetCore.Mvc;
using Noo.Api.Auth.Services;
using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.Request;
using Noo.Api.Core.Security.Authorization;
using Noo.Api.Core.Utils.Versioning;
using Noo.Api.Users.Models;
using Noo.Api.Users.Services;

namespace Noo.Api.Users;

[ApiVersion(NooApiVersions.Current)]
[ApiController]
[Route("/user")]
public class UserController : ApiController
{
    private readonly IUserService _userService;

    private readonly IMentorService _mentorService;

    private readonly IAuthService _authService;

    public UserController(
        IUserService userService,
        IMentorService mentorService,
        IAuthService authService
    )
    {
        _userService = userService;
        _mentorService = mentorService;
        _authService = authService;
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUserAsync()
    {
        var result = await _userService.GetCurrentUserAsync();

        return Ok(result);
    }

    [HttpDelete("me")]
    public async Task<IActionResult> DeleteUserAsync()
    {
        // TODO: get from claims
        var userId = Ulid.NewUlid();

        await _userService.DeleteUserAsync(userId);

        return NoContent();
    }

    [HttpPatch("me/email")]
    public async Task<IActionResult> UpdateEmailAsync([FromBody] string newEmail)
    {
        await _authService.RequestEmailChangeAsync(newEmail);

        return NoContent();
    }

    [HttpPatch("me/password")]
    public async Task<IActionResult> UpdatePasswordAsync([FromBody] string newPassword)
    {
        // TODO: get from claims
        var userId = Ulid.NewUlid();

        await _userService.UpdateUserPasswordAsync(userId, newPassword);

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetUsersAsync([FromQuery] Criteria<UserModel> criteria)
    {
        var (results, count) = await _userService.GetUsersAsync(criteria);

        return OkResponse((results, count));
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> GetUserByUsernameAsync([FromRoute] string username)
    {
        var result = await _userService.GetUserByUsernameOrEmailAsync(username);

        return Ok(result);
    }

    [HttpPatch("{id}/role")]
    public async Task<IActionResult> ChangeRoleAsync([FromRoute] Ulid id, [FromBody] UserRoles newRole)
    {
        await _userService.ChangeRoleAsync(id, newRole);

        return NoContent();
    }

    [HttpPatch("{id}/block")]
    public async Task<IActionResult> BlockUserAsync([FromRoute] Ulid id)
    {
        await _userService.BlockUserAsync(id);

        return NoContent();
    }

    [HttpPatch("{id}/unblock")]
    public async Task<IActionResult> UnblockUserAsync([FromRoute] Ulid id)
    {
        await _userService.UnblockUserAsync(id);

        return NoContent();
    }

    [HttpGet("{id}/mentor-assignment")]
    public async Task<IActionResult> GetMentorAssignmentsAsync([FromRoute] Ulid id, [FromQuery] Criteria<MentorAssignmentModel> criteria)
    {
        var (results, count) = await _mentorService.GetAssignmentsAsync(id, criteria);

        return OkResponse((results, count));
    }
}
