using Noo.Api.Core.Security.Authorization;
using Noo.Api.NooTube.Types;

namespace Noo.Api.NooTube.Services;

public interface IVideoAccessService
{
    public Task<VideoAccessSelector> GetVideoSelectorAsync(Ulid userId, UserRoles userRole);
}
