using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Noo.Api.Core.Request;
using Noo.Api.Core.Utils.Versioning;
using Noo.Api.Media.Services;

namespace Noo.Api.Media;

[ApiVersion(NooApiVersions.Current)]
[ApiController]
[Route("media")]
public class MediaController : ApiController
{
    private readonly IMediaService _mediaService;

    public MediaController(IMediaService mediaService, IMapper mapper)
        : base(mapper)
    {
        _mediaService = mediaService;
    }

    // TODO: Implement media upload endpoint
}
