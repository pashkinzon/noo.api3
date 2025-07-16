using Noo.Api.Media.Models;

namespace Noo.Api.Media.Services;

public interface IMediaService
{
    public Task<MediaModel> UploadAsync(IFormFile file);
}
