
using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.NooTube.DTO;
using Noo.Api.NooTube.Models;
using Noo.Api.NooTube.Types;

namespace Noo.Api.NooTube.Services;

public interface INooTubeService
{
    public Task<(IEnumerable<NooTubeVideoDTO>, int)> GetVideosAsync(Criteria<NooTubeVideoModel> criteria, VideoAccessSelector selector);
}
