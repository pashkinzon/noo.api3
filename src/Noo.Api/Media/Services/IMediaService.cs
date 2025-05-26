using Noo.Api.Media.DTO;

namespace Noo.Api.Media.Services;

public interface IMediaService
{
    public Task<UploadMediaResponseDTO> UploadAsync(IFormFile file);
}
