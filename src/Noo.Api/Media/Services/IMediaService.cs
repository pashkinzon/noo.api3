using Noo.Api.Media.DTO;

namespace Noo.Api.Media.Services;

public interface IMediaService
{
    public Task<MediaDTO> UploadAsync(IFormFile file);
}
