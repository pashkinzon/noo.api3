using Noo.Api.Core.DataAbstraction.Model;

namespace Noo.Api.Core.DataAbstraction.Db;

public interface ISearchStrategy<T> where T : BaseModel
{
    public IQueryable<T> Apply(IQueryable<T> query, string needle);
}
