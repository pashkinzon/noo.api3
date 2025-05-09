using Noo.Api.Core.DataAbstraction.Criteria.Filters;

namespace Noo.Api.Core.DataAbstraction.Criteria.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class FilterableAttribute : Attribute
{
    public FilterType[] AllowedFilterTypes { get; }

    public FilterableAttribute(params FilterType[] allowedOperators)
    {
        AllowedFilterTypes = allowedOperators;
    }
}
