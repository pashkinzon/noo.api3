namespace Noo.Api.Core.DataAbstraction.Criteria.Filters;

public static class FilterParser
{
    public static Filter Parse(string rawFilter)
    {
        // TODO: Implement filter parsing
        return new Filter(rawFilter, FilterType.Equals);
    }
}
