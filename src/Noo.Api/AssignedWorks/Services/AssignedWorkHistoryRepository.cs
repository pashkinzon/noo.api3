using Microsoft.EntityFrameworkCore;
using Noo.Api.AssignedWorks.Models;
using Noo.Api.Core.DataAbstraction.Db;

namespace Noo.Api.AssignedWorks.Services;

public class AssignedWorkHistoryRepository : Repository<AssignedWorkStatusHistoryModel>, IAssignedWorkHistoryRepository
{
    public async Task<IEnumerable<AssignedWorkStatusHistoryModel>> GetHistoryAsync(Ulid assignedWorkId)
    {
        return await Context.Set<AssignedWorkStatusHistoryModel>()
            .Where(x => x.AssignedWorkId == assignedWorkId)
            .ToListAsync();
    }
}

public static class IUnitOfWorkAssignedWorkHistoryRepositoryExtensions
{
    public static IAssignedWorkHistoryRepository AssignedWorkHistoryRepository(this IUnitOfWork unitOfWork)
    {
        return new AssignedWorkHistoryRepository()
        {
            Context = unitOfWork.Context,
        };
    }
}
