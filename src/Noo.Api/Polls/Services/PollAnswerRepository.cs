using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Polls.Models;

namespace Noo.Api.Polls.Services;

public class PollAnswerRepository : Repository<PollAnswerModel>, IPollAnswerRepository;

public static class IUnitOfWorkPollAnswerRepositoryExtension
{
    public static IPollAnswerRepository PollAnswerRepository(this IUnitOfWork unitOfWork)
    {
        return new PollAnswerRepository()
        {
            Context = unitOfWork.Context,
        };
    }
}
