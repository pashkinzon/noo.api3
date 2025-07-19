
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.NooTube.Filters;
using Noo.Api.NooTube.Models;
using Noo.Api.NooTube.Types;

namespace Noo.Api.NooTube.Services;

public interface INooTubeService
{
    public Task<SearchResult<NooTubeVideoModel>> GetVideosAsync(NooTubeVideoFilter filter, VideoAccessSelector selector);
}
