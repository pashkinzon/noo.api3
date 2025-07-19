using AutoMapper;
using Noo.Api.AssignedWorks.DTO;
using Noo.Api.AssignedWorks.Filters;
using Noo.Api.AssignedWorks.Models;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Exceptions.Http;
using Noo.Api.Core.Utils.DI;
using Noo.Api.Users.Services;

namespace Noo.Api.AssignedWorks.Services;

[RegisterScoped(typeof(IAssignedWorkService))]
public class AssignedWorkService : IAssignedWorkService
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    private readonly IAssignedWorkRepository _assignedWorkRepository;

    private readonly IUserRepository _userRepository;

    public AssignedWorkService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _assignedWorkRepository = _unitOfWork.AssignedWorkRepository();
        _userRepository = _unitOfWork.UserRepository();
    }

    public async Task AddHelperMentorToAssignedWorkAsync(Ulid assignedWorkId, AddHelperMentorOptionsDTO options)
    {
        var assignedWork = await _assignedWorkRepository.GetByIdAsync(assignedWorkId);

        if (assignedWork == null)
        {
            throw new NotFoundException();
        }

        var mentorExists = await _userRepository.MentorExistsAsync(options.MentorId);

        if (!mentorExists)
        {
            throw new NotFoundException();
        }

        assignedWork.HelperMentorId = options.MentorId;

        if (options.NotifyStudent)
        {
            // TODO: notify student about the new helper mentor
        }

        if (options.NotifyMentor)
        {
            // TODO: notify mentor about being added as a helper mentor
        }

        await _unitOfWork.CommitAsync();
    }

    public Task ArchiveAssignedWorkAsync(Ulid assignedWorkId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAssignedWorkAsync(Ulid assignedWorkId)
    {
        throw new NotImplementedException();
    }

    public Task<AssignedWorkModel?> GetAssignedWorkAsync(Ulid assignedWorkId)
    {
        throw new NotImplementedException();
    }

    public Task<AssignedWorkProgressDTO> GetAssignedWorkProgressAsync(Ulid assignedWorkId)
    {
        throw new NotImplementedException();
    }

    public Task<SearchResult<AssignedWorkDTO>> GetMentorAssignedWorksAsync(Ulid mentorId, AssignedWorkFilter filter)
    {
        throw new NotImplementedException();
    }

    public Task<SearchResult<AssignedWorkDTO>> GetStudentAssignedWorksAsync(Ulid studentId, AssignedWorkFilter filter)
    {
        throw new NotImplementedException();
    }

    public Task MarkAssignedWorkAsCheckedAsync(Ulid assignedWorkId)
    {
        throw new NotImplementedException();
    }

    public Task MarkAssignedWorkAsSolvedAsync(Ulid assignedWorkId)
    {
        throw new NotImplementedException();
    }

    public Task<Ulid> RemakeAssignedWorkAsync(Ulid assignedWorkId, RemakeAssignedWorkOptionsDTO options)
    {
        throw new NotImplementedException();
    }

    public Task ReplaceMainMentorOfAssignedWorkAsync(Ulid assignedWorkId, ReplaceMainMentorOptionsDTO options)
    {
        throw new NotImplementedException();
    }

    public Task ReturnAssignedWorkToCheckAsync(Ulid assignedWorkId)
    {
        throw new NotImplementedException();
    }

    public Task ReturnAssignedWorkToSolveAsync(Ulid assignedWorkId)
    {
        throw new NotImplementedException();
    }

    public Task<Ulid> SaveAnswerAsync(Ulid assignedWorkId, UpsertAssignedWorkAnswerDTO answer)
    {
        throw new NotImplementedException();
    }

    public Task<Ulid> SaveCommentAsync(Ulid assignedWorkId, UpsertAssignedWorkCommentDTO comment)
    {
        throw new NotImplementedException();
    }

    public Task ShiftDeadlineOfAssignedWorkAsync(Ulid assignedWorkId, ShiftAssignedWorkDeadlineOptionsDTO options)
    {
        throw new NotImplementedException();
    }

    public Task UnarchiveAssignedWorkAsync(Ulid assignedWorkId)
    {
        throw new NotImplementedException();
    }
}
