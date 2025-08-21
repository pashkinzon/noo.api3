using Noo.Api.AssignedWorks.DTO;
using Noo.Api.AssignedWorks.Models;
using Noo.Api.AssignedWorks.Types;
using Noo.Api.Core.DataAbstraction.Db;

namespace Noo.Api.AssignedWorks.Services;

public interface IAssignedWorkRepository : IRepository<AssignedWorkModel>
{
    public Task<AssignedWorkProgressDTO?> GetProgressAsync(Ulid assignedWorkId, Ulid? userId);
    public Task<AssignedWorkModel?> GetAsync(Ulid assignedWorkId);
    public Task<AssignedWorkModel?> GetAsync(Ulid assignedWorkId, Ulid? userId);
    public Task<bool> IsMentorOwnWorkAsync(Ulid assignedWorkId, Ulid userId);
    public Task<bool> IsStudentOwnWorkAsync(Ulid assignedWorkId, Ulid userId);
    public Task<bool> IsWorkCheckStatusAsync(Ulid assignedWorkId, params AssignedWorkCheckStatus[] statuses);
    public Task<bool> IsWorkSolveStatusAsync(Ulid assignedWorkId, params AssignedWorkSolveStatus[] statuses);
    public Task<AssignedWorkModel?> GetWithTasksAndAnswersAsync(Ulid assignedWorkId);
    public Task<AssignedWorkModel?> GetWithStudentAsync(Ulid assignedWorkId);
}
