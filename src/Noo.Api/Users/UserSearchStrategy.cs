using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Utils.DI;
using Noo.Api.Users.Models;

namespace Noo.Api.Users;

[RegisterTransient]
public class UserSearchStrategy : ISearchStrategy<UserModel>
{
    public IQueryable<UserModel> Apply(IQueryable<UserModel> query, string needle)
    {
        if (string.IsNullOrWhiteSpace(needle))
        {
            return query;
        }

        var trimmedNeedle = needle.Trim();

        return query.Where(x =>
            x.Username.Contains(trimmedNeedle, StringComparison.OrdinalIgnoreCase) ||
            x.Email.Contains(trimmedNeedle, StringComparison.OrdinalIgnoreCase) ||
            x.Name.Contains(trimmedNeedle, StringComparison.OrdinalIgnoreCase));
    }
}
