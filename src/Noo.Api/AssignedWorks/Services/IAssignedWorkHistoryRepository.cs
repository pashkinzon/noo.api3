using Noo.Api.AssignedWorks.Models;
using Noo.Api.Core.DataAbstraction.Db;

namespace Noo.Api.AssignedWorks.Services;

public interface IAssignedWorkHistoryRepository : IRepository<AssignedWorkStatusHistoryModel>
{
    public Task<IEnumerable<AssignedWorkStatusHistoryModel>> GetHistoryAsync(Ulid assignedWorkId);
}

