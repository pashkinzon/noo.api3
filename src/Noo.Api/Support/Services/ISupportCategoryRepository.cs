using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Support.Models;

namespace Noo.Api.Support.Services;

public interface ISupportCategoryRepository : IRepository<SupportCategoryModel>
{
    public Task<IEnumerable<SupportCategoryModel>> GetCategoryTreeAsync(bool includeInactive = false);
}
