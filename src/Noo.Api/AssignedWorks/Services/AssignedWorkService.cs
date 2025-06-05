using Noo.Api.AssignedWorks.DTO;
using Noo.Api.AssignedWorks.Models;
using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.Security.Authorization;
using Noo.Api.Core.Utils.DI;

namespace Noo.Api.AssignedWorks.Services;

[RegisterScoped(typeof(IAssignedWorkService))]
public class AssignedWorkService : IAssignedWorkService
{
    public Task AddHelperMentorToAssignedWorkAsync(Ulid assignedWorkId, Ulid userId, AddHelperMentorOptionsDTO options)
    {
        throw new NotImplementedException();
    }

    public Task ArchiveAssignedWorkAsync(Ulid assignedWorkId, Ulid userId, UserRoles userRole)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAssignedWorkAsync(Ulid assignedWorkId, Ulid userId, UserRoles userRole)
    {
        throw new NotImplementedException();
    }

    public Task<AssignedWorkDTO?> GetAssignedWorkAsync(Ulid assignedWorkId, Ulid userId, UserRoles userRole)
    {
        throw new NotImplementedException();
    }

    public Task<AssignedWorkProgressDTO> GetAssignedWorkProgressAsync(Ulid assignedWorkId, Ulid userId, UserRoles userRole)
    {
        throw new NotImplementedException();
    }

    public Task<(IEnumerable<AssignedWorkDTO>, int)> GetMentorAssignedWorksAsync(Ulid mentorId, Criteria<AssignedWorkModel> criteria)
    {
        throw new NotImplementedException();
    }

    public Task<(IEnumerable<AssignedWorkDTO>, int)> GetStudentAssignedWorksAsync(Ulid studentId, Criteria<AssignedWorkModel> criteria)
    {
        throw new NotImplementedException();
    }

    public Task MarkAssignedWorkAsCheckedAsync(Ulid assignedWorkId, Ulid userId)
    {
        throw new NotImplementedException();
    }

    public Task MarkAssignedWorkAsSolvedAsync(Ulid assignedWorkId, Ulid userId)
    {
        throw new NotImplementedException();
    }

    public Task<Ulid> RemakeAssignedWorkAsync(Ulid assignedWorkId, Ulid userId, UserRoles userRole, RemakeAssignedWorkOptionsDTO options)
    {
        throw new NotImplementedException();
    }

    public Task ReplaceMainMentorOfAssignedWorkAsync(Ulid assignedWorkId, ReplaceMainMentorOptionsDTO options)
    {
        throw new NotImplementedException();
    }

    public Task ReturnAssignedWorkToCheckAsync(Ulid assignedWorkId, Ulid userId, UserRoles userRole)
    {
        throw new NotImplementedException();
    }

    public Task ReturnAssignedWorkToSolveAsync(Ulid assignedWorkId, Ulid userId, UserRoles userRole)
    {
        throw new NotImplementedException();
    }

    public Task<Ulid> SaveAnswerAsync(Ulid assignedWorkId, Ulid userId, UserRoles userRole, UpsertAssignedWorkAnswerDTO answer)
    {
        throw new NotImplementedException();
    }

    public Task<Ulid> SaveCommentAsync(Ulid assignedWorkId, Ulid userId, UserRoles userRole, UpsertAssignedWorkCommentDTO comment)
    {
        throw new NotImplementedException();
    }

    public Task ShiftDeadlineOfAssignedWorkAsync(Ulid assignedWorkId, Ulid userId, UserRoles userRole, ShiftAssignedWorkDeadlineOptionsDTO options)
    {
        throw new NotImplementedException();
    }

    public Task UnarchiveAssignedWorkAsync(Ulid assignedWorkId, Ulid userId, UserRoles userRole)
    {
        throw new NotImplementedException();
    }
}
