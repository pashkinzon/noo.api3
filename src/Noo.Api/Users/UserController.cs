using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Noo.Api.Auth.Services;
using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.Exceptions;
using Noo.Api.Core.Exceptions.Http;
using Noo.Api.Core.Request;
using Noo.Api.Core.Response;
using Noo.Api.Core.Security.Authorization;
using Noo.Api.Core.Utils.Versioning;
using Noo.Api.Users.DTO;
using Noo.Api.Users.Models;
using Noo.Api.Users.Services;
using ProducesAttribute = Noo.Api.Core.Documentation.ProducesAttribute;

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

    /// <summary>
    /// Retrieves the current user's information based on the authenticated user's ID.
    /// </summary>
    [HttpGet("me")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = UserPolicies.CanGetSelf)]
    [Produces(
        typeof(ApiResponseDTO<UserDTO>), StatusCodes.Status200OK,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden,
        StatusCodes.Status404NotFound
    )]
    public async Task<IActionResult> GetMeAsync()
    {
        var result = await _userService.GetUserByIdAsync(User.GetId());

        if (result is null)
        {
            throw new NotFoundException();
        }

        return OkResponse(result);
    }

    /// <summary>
    /// Deletes the current user's account.
    /// It won't delete the user from the database, but mark it as deleted and remove all personal data.
    /// </summary>
    [HttpDelete("me")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = UserPolicies.CanDeleteSelf)]
    [Produces(
        null, StatusCodes.Status204NoContent,
        StatusCodes.Status401Unauthorized
    )]
    public async Task<IActionResult> DeleteMeAsync()
    {
        var userId = User.GetId();

        await _userService.DeleteUserAsync(userId);

        return NoContent();
    }

    /// <summary>
    /// Updates the current user's email address.
    /// </summary>
    [HttpPatch("me/email")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = UserPolicies.CanPatchSelf)]
    [Produces(
        null, StatusCodes.Status204NoContent,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden,
        StatusCodes.Status404NotFound,
        StatusCodes.Status409Conflict
    )]
    public async Task<IActionResult> UpdateMyEmailAsync([FromBody] UpdateEmailDTO dto)
    {
        await _authService.RequestEmailChangeAsync(User.GetId(), dto.Email);

        return NoContent();
    }

    /// <summary>
    /// Updates the current user's password.
    /// </summary>
    [HttpPatch("me/password")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = UserPolicies.CanPatchSelf)]
    [Produces(
        null, StatusCodes.Status204NoContent,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden,
        StatusCodes.Status404NotFound
    )]
    public async Task<IActionResult> UpdatePasswordAsync([FromBody] UpdatePasswordDTO dto)
    {
        var userId = User.GetId();

        await _userService.UpdateUserPasswordAsync(userId, dto.Password);

        return NoContent();
    }

    /// <summary>
    /// Updates the current user's Telegram data like username, avatar and telegram ID
    /// </summary>
    [HttpPatch("me/telegram")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = UserPolicies.CanPatchSelf)]
    [Produces(
        null, StatusCodes.Status204NoContent,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden,
        StatusCodes.Status404NotFound,
        StatusCodes.Status409Conflict
    )]
    public async Task<IActionResult> UpdateTelegramAsync([FromBody] UpdateTelegramDTO updateTelegramDTO)
    {
        var userId = User.GetId();

        await _userService.UpdateTelegramAsync(userId, updateTelegramDTO);

        return NoContent();
    }

    [MapToApiVersion(NooApiVersions.Current)]
    [HttpPatch("me/avatar")]
    [Authorize(Policy = UserPolicies.CanPatchSelf)]
    [Produces(
        null, StatusCodes.Status204NoContent,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden,
        StatusCodes.Status404NotFound
    )]
    public async Task<IActionResult> UpdateAvatarAsync([FromForm] UpdateAvatarDTO updateAvatarDTO)
    {
        var userId = User.GetId();

        await _userService.UpdateAvatarAsync(userId, updateAvatarDTO);

        return NoContent();
    }

    /// <summary>
    /// Retrieves a list of users based on the provided search criteria.
    /// </summary>
    [MapToApiVersion(NooApiVersions.Current)]
    [HttpGet]
    [Authorize(Policy = UserPolicies.CanSearchUsers)]
    [Produces(
        typeof(ApiResponseDTO<IEnumerable<UserDTO>>), StatusCodes.Status200OK,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> GetUsersAsync([FromQuery] Criteria<UserModel> criteria)
    {
        var (results, count) = await _userService.GetUsersAsync(criteria);

        return OkResponse((results, count));
    }

    /// <summary>
    /// Retrieves a user by their unique username
    /// </summary>
    [HttpGet("{username}")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = UserPolicies.CanGetUser)]
    [Produces(
        typeof(ApiResponseDTO<UserDTO>), StatusCodes.Status200OK,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden,
        StatusCodes.Status404NotFound
    )]
    public async Task<IActionResult> GetUserByUsernameAsync([FromRoute] string username)
    {
        var result = await _userService.GetUserByUsernameOrEmailAsync(username);

        if (result is null)
        {
            throw new NotFoundException();
        }

        return OkResponse(result);
    }

    /// <summary>
    /// Changes the role of a user by their unique identifier.
    /// Only possible to change role if the user is a student, otherwise it will throw a conflict error.
    /// </summary>
    [HttpPatch("{id}/role")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = UserPolicies.CanChangeRole)]
    [Produces(
        null, StatusCodes.Status204NoContent,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden,
        StatusCodes.Status404NotFound,
        StatusCodes.Status409Conflict
    )]
    public async Task<IActionResult> ChangeRoleAsync([FromRoute] Ulid id, [FromBody] UserRoles newRole)
    {
        await _userService.ChangeRoleAsync(id, newRole);

        return NoContent();
    }

    /// <summary>
    /// Blocks a user by their unique identifier.
    /// </summary>
    [HttpPatch("{id}/block")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = UserPolicies.CanBlockUser)]
    [Produces(
        null, StatusCodes.Status204NoContent,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden,
        StatusCodes.Status404NotFound
    )]
    public async Task<IActionResult> BlockUserAsync([FromRoute] Ulid id)
    {
        await _userService.BlockUserAsync(id);

        return NoContent();
    }

    /// <summary>
    /// Unblocks a user by their unique identifier.
    /// </summary>
    [HttpPatch("{id}/unblock")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = UserPolicies.CanBlockUser)]
    [Produces(
        null, StatusCodes.Status204NoContent,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden,
        StatusCodes.Status404NotFound
    )]
    public async Task<IActionResult> UnblockUserAsync([FromRoute] Ulid id)
    {
        await _userService.UnblockUserAsync(id);

        return NoContent();
    }

    /// <summary>
    /// Verifies a user manually by their unique identifier.
    /// </summary>
    [HttpPatch("{id}/verify-manual")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = UserPolicies.CanVerifyUser)]
    [Produces(
        null, StatusCodes.Status204NoContent,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden,
        StatusCodes.Status404NotFound
    )]
    public async Task<IActionResult> VerifyUserAsync([FromRoute] Ulid id)
    {
        await _userService.VerifyUserAsync(id);

        return NoContent();
    }

    /// <summary>
    /// Retrieves a student's mentor assignments by their unique identifier.
    /// </summary>
    [HttpGet("{id}/mentor-assignment")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = UserPolicies.CanGetUser)]
    [Produces(
        typeof(ApiResponseDTO<IEnumerable<MentorAssignmentDTO>>), StatusCodes.Status200OK,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> GetMentorAssignmentsAsync([FromRoute] Ulid id, [FromQuery] Criteria<MentorAssignmentModel> criteria)
    {
        var (results, count) = await _mentorService.GetMentorAssignmentsAsync(id, criteria);

        return OkResponse((results, count));
    }

    /// <summary>
    /// Retrieves a mentor's assignments by their unique identifier.
    /// </summary>
    [HttpGet("{id}/student-assignment")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = UserPolicies.CanGetUser)]
    [ProducesResponseType(typeof(ApiResponseDTO<IEnumerable<MentorAssignmentDTO>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(SerializedNooException), StatusCodes.Status403Forbidden)]
    [Produces(
        typeof(ApiResponseDTO<IEnumerable<MentorAssignmentDTO>>), StatusCodes.Status200OK,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> GetStudentAssignmentsAsync([FromRoute] Ulid id, [FromQuery] Criteria<MentorAssignmentModel> criteria)
    {
        var (results, count) = await _mentorService.GetStudentAssignmentsAsync(id, criteria);

        return OkResponse((results, count));
    }

    /// <summary>
    /// Assigns a mentor to a student for a specific subject.
    /// </summary>
    [HttpPatch("{id}/assignment-mentor")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = UserPolicies.CanAssignMentor)]
    [Produces(
        typeof(ApiResponseDTO<IdResponseDTO>), StatusCodes.Status200OK,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden,
        StatusCodes.Status404NotFound,
        StatusCodes.Status409Conflict
    )]
    public async Task<IActionResult> AssignMentorAsync([FromRoute] Ulid id, [FromBody] CreateMentorAssignmentDTO assignment)
    {
        var assignmentId = await _mentorService.AssignMentorAsync(id, assignment.MentorId, assignment.SubjectId);

        return OkResponse(assignmentId);
    }

    /// <summary>
    /// Unassigns a mentor from a student.
    /// </summary>
    [HttpPatch("{id}/unassign-mentor")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = UserPolicies.CanAssignMentor)]
    [Produces(
        null, StatusCodes.Status204NoContent,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden,
        StatusCodes.Status404NotFound
    )]
    public async Task<IActionResult> UnassignMentorAsync([FromRoute] Ulid id)
    {
        await _mentorService.UnassignMentorAsync(id);

        return NoContent();
    }
}
