using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Support.Models;

namespace Noo.Api.Support.Services;

public class SupportCategoryRepository : Repository<SupportCategoryModel>, ISupportCategoryRepository
{
    public async Task<IEnumerable<SupportCategoryModel>> GetCategoryTreeAsync(bool includeInactive = false)
    {
        return await Context.GetDbSet<SupportCategoryModel>()
            .Where(c => c.ParentId == null)
            .Where(c => includeInactive || c.IsActive)
            .OrderBy(c => c.Order)
            .Include(c => c.Children)
            .Include(c => c.Articles)
            .AsNoTracking()
            .ToListAsync();
    }
}

public static class IUnitOfWorkSupportCategoryRepositoryExtensions
{
    public static ISupportCategoryRepository SupportCategoryRepository(this IUnitOfWork unitOfWork)
    {
        return new SupportCategoryRepository()
        {
            Context = unitOfWork.Context
        };
    }
}
