using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.DataAbstraction.Criteria.Filters;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Utils.DI;
using Noo.Api.Sessions.DTO;
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

        var sessionModel = context.AsSessionModel(userId);

        try
        {
            _sessionRepository.Add(sessionModel);
            await _unitOfWork.CommitAsync();
        }
        catch (DbUpdateException)
        { }

        return sessionModel.Id;
    }

    public Task DeleteAllSessionsAsync(Ulid userId)
    {
        _sessionRepository.DeleteAllSessionsAsync(userId);
        return _unitOfWork.CommitAsync();
    }

    public Task DeleteSessionAsync(Ulid sessionId, Ulid userId)
    {
        _sessionRepository.DeleteSessionAsync(sessionId, userId);
        return _unitOfWork.CommitAsync();
    }

    public async Task<IEnumerable<SessionDTO>> GetSessionsAsync(Ulid userId)
    {
        var criteria = new Criteria<SessionModel>();

        criteria.AddFilter(nameof(SessionModel.UserId), FilterType.Equals, userId);

        var result = await _sessionRepository.GetManyAsync<SessionDTO>(criteria, _mapper.ConfigurationProvider);

        return result.Items;
    }
}
