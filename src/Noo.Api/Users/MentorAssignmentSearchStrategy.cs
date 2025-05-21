using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Utils.DI;
using Noo.Api.Users.Models;

namespace Noo.Api.Users;

[RegisterTransient]
public class MentorAssignmentSearchStrategy : ISearchStrategy<MentorAssignmentModel>
{
    public IQueryable<MentorAssignmentModel> Apply(IQueryable<MentorAssignmentModel> query, string needle)
    {
        if (string.IsNullOrWhiteSpace(needle))
        {
            return query;
        }

        var trimmedNeedle = needle.Trim();

        return query.Where(x =>
            x.Student.Name.Contains(trimmedNeedle) ||
            x.Student.Username.Contains(trimmedNeedle) ||
            x.Student.Email.Contains(trimmedNeedle) ||
            x.Mentor.Name.Contains(trimmedNeedle) ||
            x.Mentor.Username.Contains(trimmedNeedle) ||
            x.Mentor.Email.Contains(trimmedNeedle)
        );
    }
}
