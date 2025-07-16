using Noo.Api.AssignedWorks.Models;
using Noo.Api.AssignedWorks.Types;
using Noo.Api.Core.DataAbstraction.Db;

namespace Noo.Api.AssignedWorks.Services;

public interface IAssignedWorkRepository : IRepository<AssignedWorkModel>
{
    public Task<bool> IsMentorOwnWorkAsync(Ulid assignedWorkId, Ulid userId);
    public Task<bool> IsStudentOwnWorkAsync(Ulid assignedWorkId, Ulid userId);
    public Task<bool> IsWorkCheckStatusAsync(Ulid assignedWorkId, params AssignedWorkCheckStatus[] statuses);
    public Task<bool> IsWorkSolveStatusAsync(Ulid assignedWorkId, params AssignedWorkSolveStatus[] statuses);
}

