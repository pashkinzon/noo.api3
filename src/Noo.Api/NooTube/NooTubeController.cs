using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Noo.Api.Core.Request;
using Noo.Api.Core.Utils.Versioning;
using Noo.Api.NooTube.Services;

namespace Noo.Api.NooTube;

[ApiVersion(NooApiVersions.Current)]
[ApiController]
[Route("nootube")]
public class NootubeController : ApiController
{
    private readonly INooTubeService _nootubeService;

    private readonly IVideoAccessService _videoAccessService;

    public NootubeController(INooTubeService nootubeService, IVideoAccessService videoAccessService, IMapper mapper)
        : base(mapper)
    {
        _nootubeService = nootubeService;
        _videoAccessService = videoAccessService;
    }

    /* [MapToApiVersion(NooApiVersions.Current)]
    [HttpGet]
    [Authorize(Policy = NooTubePolicies.CanGetNooTubeVideos)]
    [Produces(
        typeof(ApiResponseDTO<IEnumerable<NooTubeVideoDTO>>), StatusCodes.Status200OK,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden
    )]
    public async Task<IActionResult> GetVideosAsync(
        [FromQuery] NooTubeVideoFilter filter
    )
    {
        var userId = User.GetId();
        var userRole = User.GetRole();

        var selector = await _videoAccessService.GetVideoSelectorAsync(
            userId, userRole
        );

        var result = await _nootubeService.GetVideosAsync(filter, selector);

        return SendResponse<NooTubeVideoModel, NooTubeVideoDTO>(result);
    } */
}
