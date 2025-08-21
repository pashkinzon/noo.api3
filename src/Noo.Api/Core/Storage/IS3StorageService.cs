namespace Noo.Api.Core.Storage;

public interface IS3StorageService
{
    public string GetPreSignedUploadUrl(string key, string contentType, bool isPublic, int expiresMinutes = 15);
    public string GetPreSignedDownloadUrl(string key, int expiresMinutes = 10);
}
