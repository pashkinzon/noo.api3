using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Noo.Api.Core.Request;
using Noo.Api.Core.Response;
using Noo.Api.Core.Utils.Versioning;
using Noo.Api.Platform.DTO;
using Noo.Api.Platform.Services;
using ProducesAttribute = Noo.Api.Core.Documentation.ProducesAttribute;

namespace Noo.Api.Platform;

[ApiVersion(NooApiVersions.Current)]
[ApiController]
[Route("platform")]
public class PlatformController : ApiController
{
    private readonly IPlatformService _platformService;

    public PlatformController(IPlatformService platformService, IMapper mapper) : base(mapper)
    {
        _platformService = platformService;
    }

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
        var version = _platformService.GetPlatformVersion();

        return SendResponse(version);
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
        var changeLog = _platformService.GetChangelog();

        return SendResponse(changeLog);
    }
}
