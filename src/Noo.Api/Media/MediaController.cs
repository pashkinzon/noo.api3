using Microsoft.AspNetCore.Mvc;
using Noo.Api.Core.Exceptions.Http;
using Noo.Api.Core.Request;
using Noo.Api.Core.Utils.Versioning;
using Noo.Api.Media.DTO;
using Noo.Api.Media.Services;
using Noo.Api.Media.Types;

namespace Noo.Api.Media;

[ApiVersion(NooApiVersions.Current)]
[ApiController]
[Route("media")]
public class MediaController : ApiController
{
    private readonly IMediaService _mediaService;

    public MediaController(IMediaService mediaService)
    {
        _mediaService = mediaService;
    }
}
