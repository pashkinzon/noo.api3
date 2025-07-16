
using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.NooTube.Models;
using Noo.Api.NooTube.Types;

namespace Noo.Api.NooTube.Services;

public interface INooTubeService
{
    public Task<SearchResult<NooTubeVideoModel>> GetVideosAsync(Criteria<NooTubeVideoModel> criteria, VideoAccessSelector selector);
}
