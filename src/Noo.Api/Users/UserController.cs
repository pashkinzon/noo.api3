using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Noo.Api.Auth.Services;
using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.Exceptions.Http;
using Noo.Api.Core.Request;
using Noo.Api.Core.Response;
using Noo.Api.Core.Security.Authorization;
using Noo.Api.Core.Utils.Versioning;
using Noo.Api.Users.DTO;
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
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = UserPolicies.CanGetSelf)]
    [ProducesResponseType(typeof(ApiResponseDTO<UserDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMeAsync()
    {
        var result = await _userService.GetUserByIdAsync(User.GetId());

        if (result is null)
        {
            throw new NotFoundException();
        }

        return OkResponse(result);
    }

    [HttpDelete("me")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = UserPolicies.CanDeleteSelf)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteMeAsync()
    {
        var userId = User.GetId();

        await _userService.DeleteUserAsync(userId);

        return NoContent();
    }

    [HttpPatch("me/email")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = UserPolicies.CanPatchSelf)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateMyEmailAsync([FromBody] UpdateEmailDTO dto)
    {
        await _authService.RequestEmailChangeAsync(User.GetId(), dto.Email);

        return NoContent();
    }

    [HttpPatch("me/password")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = UserPolicies.CanPatchSelf)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePasswordAsync([FromBody] UpdatePasswordDTO dto)
    {
        var userId = User.GetId();

        await _userService.UpdateUserPasswordAsync(userId, dto.Password);

        return NoContent();
    }

    [HttpPatch("me/telegram")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = UserPolicies.CanPatchSelf)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateTelegramAsync([FromBody] UpdateTelegramDTO updateTelegramDTO)
    {
        var userId = User.GetId();

        await _userService.UpdateTelegramAsync(userId, updateTelegramDTO);

        return NoContent();
    }

    [HttpGet]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = UserPolicies.CanSearchUsers)]
    [ProducesResponseType(typeof(ApiResponseDTO<IEnumerable<UserDTO>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetUsersAsync([FromQuery] Criteria<UserModel> criteria)
    {
        var (results, count) = await _userService.GetUsersAsync(criteria);

        return OkResponse((results, count));
    }

    [HttpGet("{username}")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = UserPolicies.CanGetUser)]
    [ProducesResponseType(typeof(ApiResponseDTO<UserDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserByUsernameAsync([FromRoute] string username)
    {
        var result = await _userService.GetUserByUsernameOrEmailAsync(username);

        if (result is null)
        {
            throw new NotFoundException();
        }

        return OkResponse(result);
    }

    [HttpPatch("{id}/role")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = UserPolicies.CanChangeRole)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> ChangeRoleAsync([FromRoute] Ulid id, [FromBody] UserRoles newRole)
    {
        await _userService.ChangeRoleAsync(id, newRole);

        return NoContent();
    }

    [HttpPatch("{id}/block")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = UserPolicies.CanBlockUser)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> BlockUserAsync([FromRoute] Ulid id)
    {
        await _userService.BlockUserAsync(id);

        return NoContent();
    }

    [HttpPatch("{id}/unblock")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = UserPolicies.CanBlockUser)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UnblockUserAsync([FromRoute] Ulid id)
    {
        await _userService.UnblockUserAsync(id);

        return NoContent();
    }

    [HttpPatch("{id}/verify-manual")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = UserPolicies.CanVerifyUser)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> VerifyUserAsync([FromRoute] Ulid id)
    {
        await _userService.VerifyUserAsync(id);

        return NoContent();
    }

    [HttpGet("{id}/mentor-assignment")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = UserPolicies.CanGetUser)]
    [ProducesResponseType(typeof(ApiResponseDTO<IEnumerable<MentorAssignmentDTO>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetMentorAssignmentsAsync([FromRoute] Ulid id, [FromQuery] Criteria<MentorAssignmentModel> criteria)
    {
        var (results, count) = await _mentorService.GetMentorAssignmentsAsync(id, criteria);

        return OkResponse((results, count));
    }

    [HttpGet("{id}/student-assignment")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = UserPolicies.CanGetUser)]
    [ProducesResponseType(typeof(ApiResponseDTO<IEnumerable<MentorAssignmentDTO>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetStudentAssignmentsAsync([FromRoute] Ulid id, [FromQuery] Criteria<MentorAssignmentModel> criteria)
    {
        var (results, count) = await _mentorService.GetStudentAssignmentsAsync(id, criteria);

        return OkResponse((results, count));
    }

    [HttpPatch("{id}/assignment-mentor")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = UserPolicies.CanAssignMentor)]
    [ProducesResponseType(typeof(ApiResponseDTO<IdResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> AssignMentorAsync([FromRoute] Ulid id, [FromBody] CreateMentorAssignmentDTO assignment)
    {
        var assignmentId = await _mentorService.AssignMentorAsync(id, assignment.MentorId, assignment.SubjectId);

        return OkResponse(assignmentId);
    }

    [HttpPatch("{id}/unassign-mentor")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = UserPolicies.CanAssignMentor)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UnassignMentorAsync([FromRoute] Ulid id)
    {
        await _mentorService.UnassignMentorAsync(id);

        return NoContent();
    }
}
