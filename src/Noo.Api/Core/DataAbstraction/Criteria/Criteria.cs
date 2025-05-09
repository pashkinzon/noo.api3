using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Noo.Api.Core.DataAbstraction.Criteria.Attributes;
using Noo.Api.Core.DataAbstraction.Criteria.Filters;
using Noo.Api.Core.DataAbstraction.Model;

namespace Noo.Api.Core.DataAbstraction.Criteria;

public class Criteria<T> where T : BaseModel
{
    [Range(1, int.MaxValue)]
    [FromQuery(Name = "page")]
    public int Page { get; set; } = 1;

    [Range(1, 250)]
    [FromQuery(Name = "limit")]
    public int Limit { get; set; } = 25;

    public int Skip => (Page - 1) * Limit;

    public int Take => Limit;

    [MaxLength(50)]
    [FromQuery(Name = "search")]
    public string? Search { get; set; }

    [FromQuery(Name = "sort")]
    public string? RawSort
    {
        set
        {
            if (value != null)
            {
                SetSort(value);
            }
        }
    }

    public string? Sort { get; private set; } = "id";

    [FromQuery(Name = "sortOrder")]
    public SortOrder SortOrder { get; set; } = SortOrder.Descending;

    [ModelBinder(BinderType = typeof(FilterQueryModelBinder))]
    public Dictionary<string, string> RawFilters
    {
        set
        {
            foreach (var filter in value)
            {
                AddRawFilter(filter.Key, filter.Value);
            }
        }
    }

    public ICollection<Filter> Filters { get; private set; }

    public Criteria()
    {
        Filters = [];
    }

    public void AddFilter(string field, FilterType type, params object[] values)
    {
        if (!CriteriaAttributeResolver<T>.IsFilterable(field, type))
        {
            throw new FilterNotSupportedException(field, type);
        }

        Filters.Add(new Filter(field, type, values));
    }

    public void AddRawFilter(string field, string filterValueRaw)
    {
        var filter = FilterParser.Parse(filterValueRaw);

        if (!CriteriaAttributeResolver<T>.IsFilterable(field, filter.Type))
        {
            throw new FilterNotSupportedException(field, filter.Type);
        }

        Filters.Add(filter);
    }

    public Criteria<T> SetSort(string sort, SortOrder order = SortOrder.Descending)
    {
        if (CriteriaAttributeResolver<T>.IsSortable(sort))
        {
            throw new SortNotSupportedException(sort);
        }

        Sort = sort;
        SortOrder = order;

        return this;
    }

#if DEBUG
    public override string ToString()
    {
        return $"Page: {Page}, Limit: {Limit}, Search: {Search}, Sort: {Sort}, SortOrder: {SortOrder}, Filter count: {Filters.Count}";
    }
#endif
}
