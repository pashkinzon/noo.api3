using Noo.Api.AssignedWorks.DTO;
using Noo.Api.AssignedWorks.Models;
using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.Security.Authorization;

namespace Noo.Api.AssignedWorks.Services;

public interface IAssignedWorkService
{
    public Task<(IEnumerable<AssignedWorkDTO>, int)> GetStudentAssignedWorksAsync(Ulid studentId, Criteria<AssignedWorkModel> criteria);
    public Task<(IEnumerable<AssignedWorkDTO>, int)> GetMentorAssignedWorksAsync(Ulid mentorId, Criteria<AssignedWorkModel> criteria);
    public Task<AssignedWorkDTO?> GetAssignedWorkAsync(Ulid assignedWorkId);
    public Task<AssignedWorkProgressDTO> GetAssignedWorkProgressAsync(Ulid assignedWorkId);
    public Task<Ulid> RemakeAssignedWorkAsync(Ulid assignedWorkId, RemakeAssignedWorkOptionsDTO options);
    public Task<Ulid> SaveAnswerAsync(Ulid assignedWorkId, UpsertAssignedWorkAnswerDTO answer);
    public Task<Ulid> SaveCommentAsync(Ulid assignedWorkId, UpsertAssignedWorkCommentDTO comment);
    public Task MarkAssignedWorkAsSolvedAsync(Ulid assignedWorkId);
    public Task MarkAssignedWorkAsCheckedAsync(Ulid assignedWorkId);
    public Task ArchiveAssignedWorkAsync(Ulid assignedWorkId);
    public Task UnarchiveAssignedWorkAsync(Ulid assignedWorkId);
    public Task AddHelperMentorToAssignedWorkAsync(Ulid assignedWorkId, AddHelperMentorOptionsDTO options);
    public Task ReplaceMainMentorOfAssignedWorkAsync(Ulid assignedWorkId, ReplaceMainMentorOptionsDTO options);
    public Task ShiftDeadlineOfAssignedWorkAsync(Ulid assignedWorkId, ShiftAssignedWorkDeadlineOptionsDTO options);
    public Task ReturnAssignedWorkToSolveAsync(Ulid assignedWorkId);
    public Task ReturnAssignedWorkToCheckAsync(Ulid assignedWorkId);
    public Task DeleteAssignedWorkAsync(Ulid assignedWorkId);
}
