using Noo.Api.AssignedWorks.Models;

namespace Noo.Api.AssignedWorks.Services;

public interface IAssignedWorkHistoryService
{
    public Task<IEnumerable<AssignedWorkStatusHistoryModel>> GetHistoryAsync(Ulid assignedWorkId);

    public Task PushEventAsync(AssignedWorkStatusHistoryModel @event);
}
