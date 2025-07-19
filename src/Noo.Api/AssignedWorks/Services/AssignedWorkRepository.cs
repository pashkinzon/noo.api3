using Noo.Api.AssignedWorks.Models;
using Noo.Api.AssignedWorks.Types;
using Noo.Api.Core.DataAbstraction.Db;

namespace Noo.Api.AssignedWorks.Services;

public class AssignedWorkRepository : Repository<AssignedWorkModel>, IAssignedWorkRepository
{
    public Task<bool> IsMentorOwnWorkAsync(Ulid assignedWorkId, Ulid userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsStudentOwnWorkAsync(Ulid assignedWorkId, Ulid userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsWorkCheckStatusAsync(Ulid assignedWorkId, params AssignedWorkCheckStatus[] statuses)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsWorkSolveStatusAsync(Ulid assignedWorkId, params AssignedWorkSolveStatus[] statuses)
    {
        throw new NotImplementedException();
    }
}


public static class IUnitOfWorkAssignedWorkRepositoryExtensions
{
    public static IAssignedWorkRepository AssignedWorkRepository(this IUnitOfWork unitOfWork)
    {
        return new AssignedWorkRepository()
        {
            Context = unitOfWork.Context,
        };
    }
}
