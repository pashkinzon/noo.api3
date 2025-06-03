using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Support.Models;

namespace Noo.Api.Support.Services;

public class SupportArticleRepository : Repository<SupportArticleModel>, ISupportArticleRepository;

public static class IUnitOfWorkSupportArticleRepositoryExtensions
{
    public static ISupportArticleRepository SupportArticleRepository(this IUnitOfWork unitOfWork)
    {
        return new SupportArticleRepository()
        {
            Context = unitOfWork.Context
        };
    }
}
