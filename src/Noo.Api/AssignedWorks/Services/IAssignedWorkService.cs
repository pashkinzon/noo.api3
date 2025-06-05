using Noo.Api.AssignedWorks.DTO;
using Noo.Api.AssignedWorks.Models;
using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.Security.Authorization;

namespace Noo.Api.AssignedWorks.Services;

public interface IAssignedWorkService
{
    public Task<(IEnumerable<AssignedWorkDTO>, int)> GetStudentAssignedWorksAsync(Ulid studentId, Criteria<AssignedWorkModel> criteria);
    public Task<(IEnumerable<AssignedWorkDTO>, int)> GetMentorAssignedWorksAsync(Ulid mentorId, Criteria<AssignedWorkModel> criteria);
    public Task<AssignedWorkDTO?> GetAssignedWorkAsync(Ulid assignedWorkId, Ulid userId, UserRoles userRole);
    public Task<AssignedWorkProgressDTO> GetAssignedWorkProgressAsync(Ulid assignedWorkId, Ulid userId, UserRoles userRole);
    public Task<Ulid> RemakeAssignedWorkAsync(Ulid assignedWorkId, Ulid userId, UserRoles userRole, RemakeAssignedWorkOptionsDTO options);
    public Task<Ulid> SaveAnswerAsync(Ulid assignedWorkId, Ulid userId, UserRoles userRole, UpsertAssignedWorkAnswerDTO answer);
    public Task<Ulid> SaveCommentAsync(Ulid assignedWorkId, Ulid userId, UserRoles userRole, UpsertAssignedWorkCommentDTO comment);
    public Task MarkAssignedWorkAsSolvedAsync(Ulid assignedWorkId, Ulid userId);
    public Task MarkAssignedWorkAsCheckedAsync(Ulid assignedWorkId, Ulid userId);
    public Task ArchiveAssignedWorkAsync(Ulid assignedWorkId, Ulid userId, UserRoles userRole);
    public Task UnarchiveAssignedWorkAsync(Ulid assignedWorkId, Ulid userId, UserRoles userRole);
    public Task AddHelperMentorToAssignedWorkAsync(Ulid assignedWorkId, AddHelperMentorOptionsDTO options);
    public Task ReplaceMainMentorOfAssignedWorkAsync(Ulid assignedWorkId, ReplaceMainMentorOptionsDTO options);
    public Task ShiftDeadlineOfAssignedWorkAsync(Ulid assignedWorkId, Ulid userId, UserRoles userRole, ShiftAssignedWorkDeadlineOptionsDTO options);
    public Task ReturnAssignedWorkToSolveAsync(Ulid assignedWorkId, Ulid userId, UserRoles userRole);
    public Task ReturnAssignedWorkToCheckAsync(Ulid assignedWorkId, Ulid userId, UserRoles userRole);
    public Task DeleteAssignedWorkAsync(Ulid assignedWorkId, Ulid userId, UserRoles userRole);
}
