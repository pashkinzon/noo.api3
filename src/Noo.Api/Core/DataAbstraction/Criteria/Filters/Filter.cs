namespace Noo.Api.Core.DataAbstraction.Criteria.Filters;

public class Filter
{
    public string Field { get; init; }

    public FilterType Type { get; init; } = FilterType.Equals;

    public object[] Values { get; init; } = [];

    public Filter(string field, FilterType type, params object[] values)
    {
        Field = field;
        Type = type;
        Values = values;
    }
}
