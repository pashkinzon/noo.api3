namespace Noo.Api.Media.Services;

public interface IMediaService
{
    public Task<string> GetPresignedUploadUrlAsync(string fileName, string contentType, Types.ReasonType reason, Ulid entityId, bool isPublic);

    public Task<string> GetPresignedAccessUrlAsync(string mediaId, Ulid requestorId);

    /// <summary>
    /// Confirm that an upload completed for the given media id and update final metadata (size, hash/etag).
    /// Typically called by the frontend after successful upload to S3.
    /// </summary>
    public Task UploadCompleteAsync(string mediaId, long size, string? etag = null);
}
