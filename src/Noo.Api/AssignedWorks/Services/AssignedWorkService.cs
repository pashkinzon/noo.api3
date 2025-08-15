using Noo.Api.AssignedWorks.DTO;
using Noo.Api.AssignedWorks.Exceptions;
using Noo.Api.AssignedWorks.Filters;
using Noo.Api.AssignedWorks.Models;
using Noo.Api.AssignedWorks.Types;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Exceptions.Http;
using Noo.Api.Core.Security.Authorization;
using Noo.Api.Core.Utils.DI;
using Noo.Api.Users.Services;

namespace Noo.Api.AssignedWorks.Services;

[RegisterScoped(typeof(IAssignedWorkService))]
public class AssignedWorkService : IAssignedWorkService
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IAssignedWorkRepository _assignedWorkRepository;

    private readonly IAssignedWorkAnswerRepository _assignedWorkAnswerRepository;

    private readonly IUserRepository _userRepository;

    private readonly ICurrentUser _currentUser;

    public AssignedWorkService(IUnitOfWork unitOfWork, ICurrentUser currentUser)
    {
        _unitOfWork = unitOfWork;
        _assignedWorkRepository = _unitOfWork.AssignedWorkRepository();
        _assignedWorkAnswerRepository = _unitOfWork.AssignedWorkAnswerRepository();
        _userRepository = _unitOfWork.UserRepository();
        _currentUser = currentUser;
    }

    public async Task AddHelperMentorAsync(Ulid assignedWorkId, AddHelperMentorOptionsDTO options)
    {
        var assignedWork = await _assignedWorkRepository.GetByIdAsync(assignedWorkId);

        if (assignedWork == null)
        {
            throw new NotFoundException();
        }

        if (assignedWork.MainMentorId == options.MentorId || assignedWork.HelperMentorId == options.MentorId)
        {
            return; // Mentor is already the main mentor, no need to add as helper
        }

        var mentorExists = await _userRepository.MentorExistsAsync(options.MentorId);

        if (!mentorExists)
        {
            throw new NotFoundException();
        }

        if (assignedWork.IsChecked())
        {
            throw new AssignedWorkAlreadyCheckedException();
        }

        if (assignedWork.HelperMentorId != options.MentorId && assignedWork.HelperMentorId != null)
        {
            // TODO: notify helper mentor about being removed from the work
        }

        assignedWork.HelperMentorId = options.MentorId;
        await _unitOfWork.CommitAsync();

        if (options.NotifyStudent)
        {
            // TODO: notify student about the new helper mentor
        }

        if (options.NotifyMentor)
        {
            // TODO: notify mentor about being added as a helper mentor
        }
    }

    public async Task DeleteAsync(Ulid assignedWorkId)
    {
        if (await _assignedWorkRepository.IsWorkSolveStatusAsync(assignedWorkId, AssignedWorkSolveStatus.NotSolved, AssignedWorkSolveStatus.InProgress))
        {
            _assignedWorkRepository.DeleteById(assignedWorkId);

            await _unitOfWork.CommitAsync();
        }
    }

    public async Task<AssignedWorkModel?> GetAsync(Ulid assignedWorkId)
    {
        var assignedWork = await _assignedWorkRepository.GetWithTasksAndAnswersAsync(assignedWorkId);

        if (assignedWork == null)
        {
            return null;
        }

        if (!assignedWork.IsChecked())
        {
            // TODO: load comments for submitted tasks
        }

        return assignedWork;
    }

    public Task<AssignedWorkProgressDTO?> GetProgressAsync(Ulid assignedWorkId)
    {
        return _assignedWorkRepository.GetProgressAsync(assignedWorkId, _currentUser.UserId);
    }

    public Task<SearchResult<AssignedWorkModel>> GetAssignedWorksAsync(AssignedWorkFilter filter)
    {
        return _assignedWorkRepository.SearchAsync(filter);
    }

    public async Task MarkAsCheckedAsync(Ulid assignedWorkId)
    {
        var assignedWork = await _assignedWorkRepository.GetAsync(assignedWorkId, _currentUser.UserId);

        if (assignedWork == null)
        {
            throw new NotFoundException();
        }

        if (!assignedWork.IsSolved())
        {
            throw new AssignedWorkNotSolvedException();
        }

        if (assignedWork.IsChecked())
        {
            throw new AssignedWorkAlreadyCheckedException();
        }

        assignedWork.CheckedAt = DateTime.UtcNow;
        assignedWork.CheckStatus = AssignedWorkCheckStatus.Checked;

        await _unitOfWork.CommitAsync();

        // TODO: push in history checked event

        // TODO: notify student about work being checked
    }

    public async Task MarkAsSolvedAsync(Ulid assignedWorkId)
    {
        var assignedWork = await _assignedWorkRepository.GetAsync(assignedWorkId, _currentUser.UserId);

        if (assignedWork == null)
        {
            throw new NotFoundException();
        }

        if (assignedWork.IsSolved())
        {
            throw new AssignedWorkAlreadySolvedException();
        }

        assignedWork.SolvedAt = DateTime.UtcNow;
        assignedWork.SolveStatus = AssignedWorkSolveStatus.Solved;

        await _unitOfWork.CommitAsync();

        // TODO: push in history solved event

        // TODO: notify student about work being solved
        // TODO: notify mentor about work being solved
    }

    public async Task<Ulid> RemakeAsync(Ulid assignedWorkId, RemakeAssignedWorkOptionsDTO options)
    {
        var assignedWork = await _assignedWorkRepository.GetAsync(assignedWorkId, _currentUser.UserId);

        if (assignedWork == null)
        {
            throw new NotFoundException();
        }

        if (!assignedWork.IsRemakeable())
        {
            throw new AssignedWorkNotRemakeableException();
        }

        var newAssignedWork = assignedWork.NewAttempt();

        if (options.IncludeOnlyWrongTasks)
        {
            newAssignedWork.ExcludedTaskIds = await _assignedWorkAnswerRepository.GetCorrectAnswerIdsAsync(assignedWorkId);
        }

        _assignedWorkRepository.Add(newAssignedWork);
        await _unitOfWork.CommitAsync();

        return newAssignedWork.Id;
    }

    public async Task ReplaceMainMentorAsync(Ulid assignedWorkId, ReplaceMainMentorOptionsDTO options)
    {
        var assignedWork = await _assignedWorkRepository.GetAsync(assignedWorkId, _currentUser.UserId);

        if (assignedWork == null)
        {
            throw new NotFoundException();
        }

        if (assignedWork.MainMentorId == options.MentorId || assignedWork.HelperMentorId == options.MentorId)
        {
            return; // Mentor is already the main mentor, no need to replace
        }

        if (assignedWork.IsChecked())
        {
            throw new AssignedWorkAlreadyCheckedException();
        }

        assignedWork.MainMentorId = options.MentorId;
        await _unitOfWork.CommitAsync();

        if (options.NotifyStudent)
        {
            // TODO: send a notification to the student about the mentor change
        }

        if (options.NotifyMentor)
        {
            // TODO: send a notification to the mentor about the mentor change
        }

        // TODO: push the replace mentor event to the assigned work history
    }

    public async Task ReturnToCheckAsync(Ulid assignedWorkId)
    {
        var assignedWork = await _assignedWorkRepository.GetAsync(assignedWorkId, _currentUser.UserId);

        if (assignedWork == null)
        {
            throw new NotFoundException();
        }

        if (!assignedWork.IsChecked())
        {
            throw new AssignedWorkNotCheckedException();
        }

        assignedWork.CheckedAt = null;
        assignedWork.CheckStatus = AssignedWorkCheckStatus.NotChecked;

        await _unitOfWork.CommitAsync();

        // TODO: push the return to check event to the assigned work history
    }

    public async Task ReturnToSolveAsync(Ulid assignedWorkId)
    {
        var assignedWork = await _assignedWorkRepository.GetAsync(assignedWorkId, _currentUser.UserId);

        if (assignedWork == null)
        {
            throw new NotFoundException();
        }

        if (!assignedWork.IsSolved())
        {
            throw new AssignedWorkNotSolvedException();
        }

        assignedWork.CheckedAt = null;
        assignedWork.CheckStatus = AssignedWorkCheckStatus.NotChecked;
        assignedWork.Score = null;

        assignedWork.SolvedAt = null;
        assignedWork.SolveStatus = AssignedWorkSolveStatus.InProgress;

        await _unitOfWork.CommitAsync();

        // TODO: push the return to solve event to the assigned work history
    }

    public Task<Ulid> SaveAnswerAsync(Ulid assignedWorkId, UpsertAssignedWorkAnswerDTO answer)
    {
        throw new NotImplementedException();
    }

    public Task<Ulid> SaveCommentAsync(Ulid assignedWorkId, UpsertAssignedWorkCommentDTO comment)
    {
        throw new NotImplementedException();
    }

    public async Task ShiftDeadlineAsync(Ulid assignedWorkId, ShiftAssignedWorkDeadlineOptionsDTO options)
    {
        var assignedWork = await _assignedWorkRepository.GetAsync(assignedWorkId, _currentUser.UserId);

        if (assignedWork == null)
        {
            throw new NotFoundException();
        }

        if (_currentUser.UserRole == UserRoles.Student)
        {
            AssertCorrectStudentDeadlineShift(assignedWork, options.NewDeadline);
            assignedWork.SolveDeadlineAt = options.NewDeadline;
        }
        else if (_currentUser.UserRole == UserRoles.Mentor)
        {
            AssertCorrectMentorDeadlineShift(assignedWork, options.NewDeadline);
            assignedWork.CheckDeadlineAt = options.NewDeadline;
        }
        else
        {
            throw new ForbiddenException();
        }

        await _unitOfWork.CommitAsync();

        if (options.NotifyOthers)
        {
            // TODO: send a notification to a student and to a mentor
        }

        // TODO: push the shift deadline event to the assigned work history
    }

    public async Task ArchiveAsync(Ulid assignedWorkId)
    {
        var assignedWork = await _assignedWorkRepository.GetAsync(assignedWorkId, _currentUser.UserId);

        if (assignedWork == null)
        {
            throw new NotFoundException();
        }

        switch (_currentUser.UserRole)
        {
            case UserRoles.Student:
                assignedWork.IsArchivedByStudent = true;
                break;
            case UserRoles.Mentor:
                assignedWork.IsArchivedByMentors = true;
                break;
            case UserRoles.Admin:
            case UserRoles.Assistant:
            case UserRoles.Teacher:
                assignedWork.IsArchivedByAssistants = true;
                break;
            default:
                throw new ForbiddenException();
        }

        await _unitOfWork.CommitAsync();

        // TODO: push the archive event to the assigned work history
    }

    public async Task UnarchiveAsync(Ulid assignedWorkId)
    {
        var assignedWork = await _assignedWorkRepository.GetAsync(assignedWorkId, _currentUser.UserId);

        if (assignedWork == null)
        {
            throw new NotFoundException();
        }

        switch (_currentUser.UserRole)
        {
            case UserRoles.Student:
                assignedWork.IsArchivedByStudent = false;
                break;
            case UserRoles.Mentor:
                assignedWork.IsArchivedByMentors = false;
                break;
            case UserRoles.Admin:
            case UserRoles.Assistant:
            case UserRoles.Teacher:
                assignedWork.IsArchivedByAssistants = false;
                break;
            default:
                throw new ForbiddenException();
        }

        await _unitOfWork.CommitAsync();

        // TODO: push the unarchive event to the assigned work history
    }

    private void AssertCorrectStudentDeadlineShift(AssignedWorkModel assignedWork, DateTime newDeadline)
    {
        if (newDeadline - assignedWork.SolveDeadlineAt > AssignedWorkConfig.MaxSolveDeadlineShift)
        {
            throw new IncorrectDeadlineShiftException();
        }

        if (assignedWork.IsSolved())
        {
            throw new AssignedWorkAlreadySolvedException();
        }
    }

    private void AssertCorrectMentorDeadlineShift(AssignedWorkModel assignedWork, DateTime newDeadline)
    {
        if (newDeadline - assignedWork.CheckDeadlineAt > AssignedWorkConfig.MaxCheckDeadlineShift)
        {
            throw new IncorrectDeadlineShiftException();
        }

        if (assignedWork.IsChecked())
        {
            throw new AssignedWorkAlreadyCheckedException();
        }
    }
}
