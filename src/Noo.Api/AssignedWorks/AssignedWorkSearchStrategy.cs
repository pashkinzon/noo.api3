using Noo.Api.AssignedWorks.Models;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Utils.DI;

namespace Noo.Api.AssignedWorks;

[RegisterTransient]
public class AssignedWorkSearchStrategy : ISearchStrategy<AssignedWorkModel>
{
    public IQueryable<AssignedWorkModel> Apply(IQueryable<AssignedWorkModel> query, string needle)
    {
        throw new NotImplementedException();
    }
}
