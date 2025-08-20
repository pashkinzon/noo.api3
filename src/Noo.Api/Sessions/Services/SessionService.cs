using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Exceptions.Http;
using Noo.Api.Core.Utils.DI;
using Noo.Api.Sessions.Models;
using Noo.Api.Sessions.Utils;

namespace Noo.Api.Sessions.Services;

[RegisterScoped(typeof(ISessionService))]
public class SessionService : ISessionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISessionRepository _sessionRepository;
    private readonly IMapper _mapper;

    public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _sessionRepository = _unitOfWork.SessionRepository();
    }

    public async Task<Ulid> CreateSessionIfNotExistsAsync(HttpContext context, Ulid userId)
    {
        if (context is null || context.User is null)
        {
            throw new ArgumentNullException(nameof(context), "HttpContext or User cannot be null.");
        }

        var incoming = context.AsSessionModel(userId);

        // Deduplicate: prefer deviceId when present; else fallback to user agent for browsers
        var set = _unitOfWork.Context.GetDbSet<SessionModel>();
        SessionModel? existing = null;
        if (!string.IsNullOrWhiteSpace(incoming.DeviceId))
        {
            existing = await set
                .OrderByDescending(s => s.LastRequestAt ?? s.UpdatedAt ?? s.CreatedAt)
                .FirstOrDefaultAsync(s => s.UserId == userId && s.DeviceId == incoming.DeviceId);
        }
        else if (!string.IsNullOrWhiteSpace(incoming.UserAgent))
        {
            existing = await set
                .OrderByDescending(s => s.LastRequestAt ?? s.UpdatedAt ?? s.CreatedAt)
                .FirstOrDefaultAsync(s => s.UserId == userId && s.UserAgent == incoming.UserAgent);
        }

        if (existing is null)
        {
            _sessionRepository.Add(incoming);
            await _unitOfWork.CommitAsync();
            return incoming.Id;
        }

        // Update metadata on existing session
        existing.LastRequestAt = DateTime.UtcNow;
        existing.UpdatedAt = DateTime.UtcNow;
        existing.UserAgent = incoming.UserAgent;
        existing.Browser = incoming.Browser;
        existing.Os = incoming.Os;
        existing.Device = incoming.Device;
        existing.DeviceType = incoming.DeviceType;
        existing.IpAddress = incoming.IpAddress;
        existing.DeviceId = incoming.DeviceId ?? existing.DeviceId;

        _sessionRepository.Update(existing);
        await _unitOfWork.CommitAsync();
        return existing.Id;
    }

    public Task DeleteAllSessionsAsync(Ulid userId)
    {
        _sessionRepository.DeleteAllSessionsAsync(userId);
        return _unitOfWork.CommitAsync();
    }

    public async Task DeleteSessionAsync(Ulid sessionId, Ulid userId)
    {
        // Enforce ownership strictly: only delete when the session belongs to the user.
        var set = _unitOfWork.Context.GetDbSet<SessionModel>();
        var entity = await set.FirstOrDefaultAsync(s => s.Id == sessionId && s.UserId == userId);

        if (entity is null)
        {
            // Surface as 404 to callers that care (e.g., DELETE /session/{id}).
            throw new NotFoundException();
        }

        set.Remove(entity);
        await _unitOfWork.CommitAsync();
    }

    public Task<IEnumerable<SessionModel>> GetSessionsAsync(Ulid userId)
    {
        return _sessionRepository.GetManyOfUserAsync(userId);
    }
}
