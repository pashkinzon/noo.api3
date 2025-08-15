using Noo.Api.AssignedWorks.Models;
using Noo.Api.Core.DataAbstraction.Db;

namespace Noo.Api.AssignedWorks.Services;

public interface IAssignedWorkAnswerRepository : IRepository<AssignedWorkAnswerModel>
{
    public Task<Ulid[]> GetCorrectAnswerIdsAsync(Ulid assignedWorkId);
}
