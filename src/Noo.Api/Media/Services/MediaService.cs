using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Exceptions.Http;
using Noo.Api.Core.Security.Authorization;
using Noo.Api.Core.Storage;
using Noo.Api.Core.Utils.DI;
using Noo.Api.Courses.Services;
using Noo.Api.Media.Models;
using Noo.Api.Media.Types;

namespace Noo.Api.Media.Services;

[RegisterScoped(typeof(IMediaService))]
public class MediaService : IMediaService
{
    private readonly IS3StorageService _s3;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseMembershipService _courseMembershipService;
    private readonly ICurrentUser _currentUser;

    public MediaService(IS3StorageService s3, IUnitOfWork unitOfWork, ICourseMembershipService courseMembershipService, ICurrentUser currentUser)
    {
        _s3 = s3;
        _unitOfWork = unitOfWork;
        _courseMembershipService = courseMembershipService;
        _currentUser = currentUser;
    }

    public async Task<string> GetPresignedUploadUrlAsync(string fileName, string contentType, ReasonType reason, Ulid entityId, bool isPublic)
    {
        if (!MediaConfig.AllowedFileTypes.Contains(contentType))
            throw new InvalidOperationException("File type not allowed");

        var ext = Path.GetExtension(fileName)?.TrimStart('.') ?? string.Empty;
        var key = $"media/{reason.ToString().ToLowerInvariant()}/{entityId}/{Ulid.NewUlid()}.{ext}";

        var url = _s3.GetPreSignedUploadUrl(key, contentType, isPublic);

        var media = new MediaModel
        {
            Path = key,
            Name = fileName,
            ActualName = fileName,
            Extension = ext,
            Size = 0,
            Reason = reason.ToString(),
            EntityId = entityId,
            OwnerId = _currentUser.UserId
        };

        _unitOfWork.Context.GetDbSet<MediaModel>().Add(media);
        await _unitOfWork.CommitAsync();

        return url;
    }

    public async Task<string> GetPresignedAccessUrlAsync(string mediaId, Ulid requestorId)
    {
        var parsed = Ulid.Parse(mediaId);
        var media = await _unitOfWork.Context.GetDbSet<MediaModel>().FindAsync(parsed);
        if (media == null)
            throw new InvalidOperationException("File not found");

        var isPublic = media.Path.Contains("avatar") || media.Path.Contains("thumbnail");
        if (!isPublic)
        {
            // If media is related to a course, require membership
            if (Enum.TryParse<Types.ReasonType>(media.Reason, true, out var parsedReason) &&
                (parsedReason == Types.ReasonType.Course || parsedReason == Types.ReasonType.CourseThumbnail))
            {
                if (!_currentUser.IsAuthenticated)
                    throw new UnauthorizedException();

                var requester = _currentUser.UserId ?? requestorId;

                if (!await _courseMembershipService.HasAccessAsync(media.EntityId ?? Ulid.Empty, requester))
                    throw new ForbiddenException("User is not a member of the course");
            }

            // If owner-specific media (avatar, user files) check ownership or elevated role
            if (parsedReason == Types.ReasonType.Avatar)
            {
                if (!_currentUser.IsAuthenticated)
                    throw new UnauthorizedException();

                var requester = _currentUser.UserId ?? requestorId;
                if (media.OwnerId != requester && !_currentUser.IsInRole(Core.Security.Authorization.UserRoles.Admin))
                    throw new ForbiddenException("Not owner");
            }
        }

        return _s3.GetPreSignedDownloadUrl(media.Path);
    }

    public async Task UploadCompleteAsync(string mediaId, long size, string? etag = null)
    {
        var parsed = Ulid.Parse(mediaId);
        var media = await _unitOfWork.Context.GetDbSet<MediaModel>().FindAsync(parsed);
        if (media == null)
            throw new NotFoundException("Media not found");

        // Basic permission: only owner or admin can confirm upload
        if (_currentUser.IsAuthenticated && media.OwnerId.HasValue)
        {
            if (_currentUser.UserId != media.OwnerId && !_currentUser.IsInRole(Core.Security.Authorization.UserRoles.Admin))
                throw new ForbiddenException();
        }

        media.Size = size;
        if (!string.IsNullOrEmpty(etag)) media.Hash = etag;

        _unitOfWork.Context.GetDbSet<MediaModel>().Update(media);
        await _unitOfWork.CommitAsync();
    }
}
