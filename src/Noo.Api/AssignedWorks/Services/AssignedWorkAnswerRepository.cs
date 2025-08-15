using Microsoft.EntityFrameworkCore;
using Noo.Api.AssignedWorks.Models;
using Noo.Api.Core.DataAbstraction.Db;

namespace Noo.Api.AssignedWorks.Services;

public class AssignedWorkAnswerRepository : Repository<AssignedWorkAnswerModel>, IAssignedWorkAnswerRepository
{
    public Task<Ulid[]> GetCorrectAnswerIdsAsync(Ulid assignedWorkId)
    {
        return Context.Set<AssignedWorkAnswerModel>()
            .Where(a => a.AssignedWorkId == assignedWorkId)
            .Where(a => a.Score == a.MaxScore)
            .Select(a => a.TaskId)
            .ToArrayAsync();
    }
}


public static class IUnitOfWorkAssignedWorkAnswerRepositoryExtensions
{
    public static IAssignedWorkAnswerRepository AssignedWorkAnswerRepository(this IUnitOfWork unitOfWork)
    {
        return new AssignedWorkAnswerRepository()
        {
            Context = unitOfWork.Context,
        };
    }
}
