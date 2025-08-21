using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Noo.Api.Core.Request;
using Noo.Api.Core.Response;
using Noo.Api.Core.Utils.Versioning;
using Noo.Api.Media.Services;
using ProducesAttribute = Noo.Api.Core.Documentation.ProducesAttribute;

namespace Noo.Api.Media;

[ApiVersion(NooApiVersions.Current)]
[ApiController]
[Route("media")]
public class MediaController : ApiController
{
    private readonly IMediaService _mediaService;

    public MediaController(IMediaService mediaService, IMapper mapper)
        : base(mapper)
    {
        _mediaService = mediaService;
    }

    /// <summary>
    /// Request a pre-signed upload URL for a public file (for example: user avatar or course image).
    /// The frontend should use the returned URL to PUT the file directly to S3.
    /// </summary>
    /// <param name="fileName">File name including extension.</param>
    /// <param name="contentType">MIME type of the file.</param>
    /// <param name="reason">Reason/type for the file (avatar, course, etc.).</param>
    /// <param name="entityId">Related entity id (user id, course id, etc.).</param>
    /// <returns>Pre-signed URL to upload the file.</returns>
    [MapToApiVersion(NooApiVersions.Current)]
    [HttpPost("upload-link/public")]
    [Produces(
        typeof(ApiResponseDTO<string>), StatusCodes.Status200OK,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden,
        StatusCodes.Status404NotFound
    )]
    public async Task<IActionResult> GetPublicUploadLinkAsync([
        FromQuery] string fileName,
        [FromQuery] string contentType,
        [FromQuery] Types.ReasonType reason,
        [FromQuery] Ulid entityId)
    {
        var url = await _mediaService.GetPresignedUploadUrlAsync(fileName, contentType, reason, entityId, true);
        return SendResponse(url);
    }

    /// <summary>
    /// Request a pre-signed upload URL for a private file (for example: course material).
    /// </summary>
    /// <remarks>Private files require server-side access checks for downloads.</remarks>
    [MapToApiVersion(NooApiVersions.Current)]
    [HttpPost("upload-link/private")]
    [Produces(
        typeof(ApiResponseDTO<string>), StatusCodes.Status200OK,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden,
        StatusCodes.Status404NotFound
    )]
    public async Task<IActionResult> GetPrivateUploadLinkAsync([
        FromQuery] string fileName,
        [FromQuery] string contentType,
        [FromQuery] Types.ReasonType reason,
        [FromQuery] Ulid entityId)
    {
        var url = await _mediaService.GetPresignedUploadUrlAsync(fileName, contentType, reason, entityId, false);
        return SendResponse(url);
    }

    /// <summary>
    /// Request an access link for a private file. Access is validated server-side.
    /// </summary>
    /// <param name="mediaId">Media id.</param>
    /// <param name="requestorId">Id of the user requesting access (server may also determine current user).</param>
    [MapToApiVersion(NooApiVersions.Current)]
    [HttpGet("access-link/{mediaId}")]
    [Produces(
        typeof(ApiResponseDTO<string>), StatusCodes.Status200OK,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden,
        StatusCodes.Status404NotFound
    )]
    public async Task<IActionResult> GetAccessLinkAsync([
        FromRoute] string mediaId,
        [FromQuery] Ulid requestorId)
    {
        var url = await _mediaService.GetPresignedAccessUrlAsync(mediaId, requestorId);
        return SendResponse(url);
    }

    /// <summary>
    /// Confirm that an upload completed and update stored metadata (size, optional etag/hash).
    /// Frontend should call this after successfully PUTting the file to S3.
    /// </summary>
    [MapToApiVersion(NooApiVersions.Current)]
    [HttpPost("upload-complete/{mediaId}")]
    [Produces(
        typeof(ApiResponseDTO<object>), StatusCodes.Status200OK,
        StatusCodes.Status400BadRequest,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status403Forbidden,
        StatusCodes.Status404NotFound
    )]
    public async Task<IActionResult> UploadCompleteAsync([FromRoute] string mediaId, [FromQuery] long size, [FromQuery] string? etag)
    {
        await _mediaService.UploadCompleteAsync(mediaId, size, etag);
        return SendResponse();
    }
}
