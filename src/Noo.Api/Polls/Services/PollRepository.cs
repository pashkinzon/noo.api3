using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Polls.Models;

namespace Noo.Api.Polls.Services;

public class PollRepository : Repository<PollModel>, IPollRepository;

public static class IUnitOfWorkPollRepositoryExtension
{
    public static IPollRepository PollRepository(this IUnitOfWork unitOfWork)
    {
        return new PollRepository()
        {
            Context = unitOfWork.Context,
        };
    }
}
