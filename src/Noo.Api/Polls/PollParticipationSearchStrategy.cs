using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Utils.DI;
using Noo.Api.Polls.Models;

namespace Noo.Api.Polls;

[RegisterTransient]
public class PollParticipationSearchStrategy : ISearchStrategy<PollParticipationModel>
{
    public IQueryable<PollParticipationModel> Apply(IQueryable<PollParticipationModel> query, string needle)
    {
        if (string.IsNullOrWhiteSpace(needle))
        {
            return query;
        }

        var trimmedNeedle = needle.Trim();

        // TODO: search using external user data too
        return query.Where(x =>
            x.User!.Name.Contains(trimmedNeedle, StringComparison.OrdinalIgnoreCase)
            || x.User!.Email.Contains(trimmedNeedle, StringComparison.OrdinalIgnoreCase)
            || x.User!.TelegramUsername!.Contains(trimmedNeedle, StringComparison.OrdinalIgnoreCase)
        );
    }
}
