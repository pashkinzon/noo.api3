using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.Request;
using Noo.Api.Core.Response;
using Noo.Api.Core.Utils.Versioning;
using Noo.Api.NooTube.DTO;
using Noo.Api.NooTube.Models;
using Noo.Api.NooTube.Services;
using ProducesAttribute = Noo.Api.Core.Documentation.ProducesAttribute;

namespace Noo.Api.NooTube;

[ApiVersion(NooApiVersions.Current)]
[ApiController]
[Route("nootube")]
public class NootubeController : ApiController
{
    private readonly INooTubeService _nootubeService;

    private readonly IVideoAccessService _videoAccessService;

    public NootubeController(INooTubeService nootubeService, IVideoAccessService videoAccessService)
    {
        _nootubeService = nootubeService;
        _videoAccessService = videoAccessService;
    }

    [MapToApiVersion(NooApiVersions.Current)]
    [HttpGet]
    [Authorize(Policy = NooTubePolicies.CanGetNooTubeVideos)]
    [Produces(
        typeof(ApiResponseDTO<IEnumerable<NooTubeVideoDTO>>), StatusCodes.Status200OK,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> GetVideosAsync(
        [FromQuery] Criteria<NooTubeVideoModel> criteria
    )
    {
        var userId = User.GetId();
        var userRole = User.GetRole();

        var selector = await _videoAccessService.GetVideoSelectorAsync(
            userId, userRole
        );

        var result = await _nootubeService.GetVideosAsync(criteria, selector);

        return OkResponse(result);
    }
}
