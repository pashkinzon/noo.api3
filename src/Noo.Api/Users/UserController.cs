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
using SystemTextJsonPatch;
using ProducesAttribute = Noo.Api.Core.Documentation.ProducesAttribute;

namespace Noo.Api.Users;

[ApiVersion(NooApiVersions.Current)]
[ApiController]
[Route("/user")]
public class UserController : ApiController
{
    private readonly IUserService _userService;

    private readonly IMentorService _mentorService;

    public UserController(
        IUserService userService,
        IMentorService mentorService
    )
    {
        _userService = userService;
        _mentorService = mentorService;
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
    [HttpGet("{id}")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = UserPolicies.CanGetUser)]
    [Produces(
        typeof(ApiResponseDTO<UserDTO>), StatusCodes.Status200OK,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden,
        StatusCodes.Status404NotFound
    )]
    public async Task<IActionResult> GetUserByIdAsync([FromRoute] Ulid id)
    {
        var result = await _userService.GetUserByIdAsync(id);

        if (result is null)
        {
            throw new NotFoundException();
        }

        return OkResponse(result);
    }

    /// <summary>
    /// Patches a user by their unique identifier.
    /// </summary>
    [HttpPatch("{id}")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = UserPolicies.CanPatchUser)]
    [Produces(
        null, StatusCodes.Status204NoContent,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> PatchUserAsync([FromRoute] Ulid id, [FromBody] JsonPatchDocument<UpdateUserDTO> patchUser)
    {
        await _userService.UpdateUserAsync(id, patchUser, ModelState);

        return NoContent();
    }


    /// <summary>
    /// Changes the role of a user by their unique identifier.
    /// Only possible to change role if the user is a student, otherwise it will throw a conflict error.
    /// </summary>
    /// <remarks>
    /// After role change, all the user sessions will be invalidated, the user will be logged out.
    /// </remarks>
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
    /// <remarks>
    /// If a user already has a mentor assigned for the subject, it will be unassigned and a new one will be assigned.
    /// </remarks>
    [HttpPatch("{id}/assign-mentor")]
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

    /// <summary>
    /// Deletes a user by their unique identifier.
    /// </summary>
    [HttpDelete("{id}")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = UserPolicies.CanDeleteUser)]
    [Produces(
        null, StatusCodes.Status204NoContent,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> DeleteUserAsync([FromRoute] Ulid id)
    {
        await _userService.DeleteUserAsync(id);
        return NoContent();
    }
}
