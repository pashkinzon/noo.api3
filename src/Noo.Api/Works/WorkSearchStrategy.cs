using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Utils.DI;
using Noo.Api.Works.Models;

namespace Noo.Api.Works;

[RegisterTransient]
public class WorkSearchStrategy : ISearchStrategy<WorkModel>
{
    public IQueryable<WorkModel> Apply(IQueryable<WorkModel> query, string needle)
    {
        if (string.IsNullOrWhiteSpace(needle))
        {
            return query;
        }

        var trimmedNeedle = needle.Trim();

        return query.Where(x => x.Title.Contains(trimmedNeedle, StringComparison.OrdinalIgnoreCase));
    }
}
