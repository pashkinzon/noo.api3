using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Utils.DI;
using Noo.Api.Subjects.Models;

namespace Noo.Api.Subjects;

[RegisterTransient]
public class SubjectSearchStrategy : ISearchStrategy<SubjectModel>
{
    public IQueryable<SubjectModel> Apply(IQueryable<SubjectModel> query, string needle)
    {
        return query.Where(x => x.Name.Contains(needle, StringComparison.OrdinalIgnoreCase));
    }
}
