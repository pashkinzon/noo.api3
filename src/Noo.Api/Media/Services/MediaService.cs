using Noo.Api.Core.Utils.DI;
using Noo.Api.Media.Models;

namespace Noo.Api.Media.Services;

[RegisterScoped(typeof(IMediaService))]
public class MediaService : IMediaService
{
    public Task<MediaModel> UploadAsync(IFormFile file)
    {
        throw new NotImplementedException();
    }
}
