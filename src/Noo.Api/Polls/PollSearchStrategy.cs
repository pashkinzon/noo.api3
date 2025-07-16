using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Utils.DI;
using Noo.Api.Polls.Models;

namespace Noo.Api.Polls;

[RegisterTransient]
public class PollSearchStrategy : ISearchStrategy<PollModel>
{
    public IQueryable<PollModel> Apply(IQueryable<PollModel> query, string needle)
    {
        if (string.IsNullOrWhiteSpace(needle))
        {
            return query;
        }

        var trimmedNeedle = needle.Trim();

        return query.Where(x => x.Title.Contains(trimmedNeedle, StringComparison.OrdinalIgnoreCase));
    }
}
