using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Noo.Api.Core.Request;
using Noo.Api.Core.Response;
using Noo.Api.Core.Utils.Versioning;
using Noo.Api.Platform.DTO;
using Noo.Api.Platform.Types;
using ProducesAttribute = Noo.Api.Core.Documentation.ProducesAttribute;

namespace Noo.Api.Platform;

[ApiVersion(NooApiVersions.Current)]
[ApiController]
[Route("platform")]
public class PlatformController : ApiController
{
    /// <summary>
    /// Gets the current platform version.
    /// </summary>
    [HttpGet("version")]
    [MapToApiVersion(NooApiVersions.Current)]
    [AllowAnonymous]
    [Produces(
        typeof(ApiResponseDTO<string>), StatusCodes.Status200OK,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public IActionResult GetPlatformVersion()
    {
        return OkResponse(NooApiVersions.Current);
    }

    /// <summary>
    /// Gets the changelog
    /// </summary>
    [HttpGet("changelog")]
    [MapToApiVersion(NooApiVersions.Current)]
    [Authorize(Policy = PlatformPolicies.CanViewChangelog)]
    [Produces(
        typeof(ApiResponseDTO<IEnumerable<ChangeLogDTO>>), StatusCodes.Status200OK,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public IActionResult GetChangelog()
    {
        // TODO: Replace with actual changelog retrieval logic
        var changeLog = new ChangeLogDTO
        {
            Version = NooApiVersions.Current,
            Date = DateTime.UtcNow,
            Changes = [
                new PlatformChange
                {
                    Type = ChangeType.Feature,
                    Author = "Noo Team",
                    Description = "Initial release of the Noo API platform."
                },
                new PlatformChange
                {
                    Type = ChangeType.BugFix,
                    Author = "Noo Team",
                    Description = "Updated API documentation and versioning."
                },
                new PlatformChange
                {
                    Type = ChangeType.Optimization,
                    Author = "Noo Team",
                    Description = "Deprecated old endpoints in favor of new ones."
                },
                new PlatformChange
                {
                    Type = ChangeType.Refactor,
                    Author = "Noo Team",
                    Description = "Refactored API controllers for better maintainability."
                }
            ]
        };

        return OkResponse(([changeLog], 1));
    }
}
