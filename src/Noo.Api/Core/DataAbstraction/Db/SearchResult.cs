namespace Noo.Api.Core.DataAbstraction.Db;

public record SearchResult<T>(IEnumerable<T> Items, int Total) where T : class
{
    public object Metadata => new
    {
        total = Total,
    };
}

