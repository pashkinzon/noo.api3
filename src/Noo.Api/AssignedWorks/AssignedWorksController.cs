using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Noo.Api.AssignedWorks.DTO;
using Noo.Api.AssignedWorks.Models;
using Noo.Api.AssignedWorks.Services;
using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.Request;
using Noo.Api.Core.Response;
using Noo.Api.Core.Security.Authorization;
using Noo.Api.Core.Utils.Versioning;
using ProducesAttribute = Noo.Api.Core.Documentation.ProducesAttribute;

namespace Noo.Api.AssignedWorks;

[ApiVersion(NooApiVersions.Current)]
[ApiController]
[Route("assigned-work")]
public class AssignedWorkController : ApiController
{
    private readonly IAssignedWorkService _assignedWorkService;

    public AssignedWorkController(IAssignedWorkService assignedWorkService)
    {
        _assignedWorkService = assignedWorkService;
    }

    /// <summary>
    /// Gets the current user's list of assigned works.
    /// </summary>
    [ApiVersion(NooApiVersions.Current)]
    [HttpGet]
    [Authorize(Policy = AssignedWorkPolicies.CanGetOwnAssignedWorks)]
    [Produces(
        typeof(ApiResponseDTO<IEnumerable<AssignedWorkDTO>>), StatusCodes.Status200OK,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> GetAssignedWorksAsync()
    {
        var userId = User.GetId();
        var userRole = User.GetRole();

        if (userRole == UserRoles.Student)
        {
            var result = await _assignedWorkService.GetStudentAssignedWorksAsync(userId, new Criteria<AssignedWorkModel>());

            return OkResponse(result);
        }
        else if (userRole == UserRoles.Mentor)
        {
            var result = await _assignedWorkService.GetMentorAssignedWorksAsync(userId, new Criteria<AssignedWorkModel>());

            return OkResponse(result);
        }

        return Forbid();
    }
}
