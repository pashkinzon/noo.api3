using Noo.Api.AssignedWorks.Models;
using Noo.Api.Core.DataAbstraction.Db;

namespace Noo.Api.AssignedWorks.Services;

public class AssignedWorkRepository : Repository<AssignedWorkModel>, IAssignedWorkRepository;

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
