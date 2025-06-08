using Noo.Api.Core.Utils.DI;
using Noo.Api.Media.DTO;

namespace Noo.Api.Media.Services;

[RegisterScoped(typeof(IMediaService))]
public class MediaService : IMediaService
{
    public Task<MediaDTO> UploadAsync(IFormFile file)
    {
        throw new NotImplementedException();
    }
}
