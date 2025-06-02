using Noo.Api.Core.DataAbstraction.Criteria.Filters;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.DataAbstraction.Model;
using System.Linq.Dynamic.Core;

namespace Noo.Api.Core.DataAbstraction.Criteria;

public static class CriteriaQueryBuilder
{
    public static IQueryable<T> AddCriteria<T>(this IQueryable<T> query, Criteria<T>? criteria, ISearchStrategy<T>? searchStrategy = null) where T : BaseModel
    {
        if (criteria == null)
        {
            return query;
        }

        if (searchStrategy != null && !string.IsNullOrEmpty(criteria.Search))
        {
            query = searchStrategy.Apply(query, criteria.Search);
        }

        if (criteria.Filters != null && criteria.Filters.Count != 0)
        {
            foreach (var filter in criteria.Filters)
            {
                query = query.AddFilter(filter);
            }
        }

        if (!string.IsNullOrEmpty(criteria.Sort))
        {
            query = query.AddSort(criteria.Sort, criteria.SortOrder);
        }

        return query.Skip(criteria.Skip).Take(criteria.Take);
    }

    public static IQueryable<T> AddCountingCriteria<T>(this IQueryable<T> query, Criteria<T>? criteria, ISearchStrategy<T>? searchStrategy = null) where T : BaseModel
    {
        if (criteria == null)
        {
            return query;
        }

        if (criteria.Filters != null && criteria.Filters.Count != 0)
        {
            foreach (var filter in criteria.Filters)
            {
                query = query.AddFilter(filter);
            }
        }

        if (searchStrategy != null && !string.IsNullOrEmpty(criteria.Search))
        {
            query = searchStrategy.Apply(query, criteria.Search);
        }

        return query;
    }

    private static IQueryable<T> AddSort<T>(this IQueryable<T> query, string sort, SortOrder order) where T : BaseModel
    {
        var sortString = $"{sort} {(order == SortOrder.Ascending ? "ASC" : "DESC")}";

        return query.OrderBy(sortString);
    }

    private static IQueryable<T> AddFilter<T>(this IQueryable<T> query, Filter filter) where T : BaseModel
    {
        switch (filter.Type)
        {
            case FilterType.Equals:
                AddEqualsFilter(ref query, filter);
                break;
            case FilterType.NotEquals:
                AddNotEqualsFilter(ref query, filter);
                break;
            case FilterType.Contains:
                AddContainsFilter(ref query, filter);
                break;
            case FilterType.Array:
                AddArrayFilter(ref query, filter);
                break;
            case FilterType.Range:
                AddRangeFilter(ref query, filter);
                break;
            default:
                throw new FilterNotSupportedException(filter.Field, filter.Type);
        }

        return query;
    }

    private static void AddEqualsFilter<T>(ref IQueryable<T> query, Filter filter) where T : BaseModel
    {
        query = query.Where($"{filter.Field} = {filter.Values[0]}");
    }

    private static void AddNotEqualsFilter<T>(ref IQueryable<T> query, Filter filter) where T : BaseModel
    {
        query = query.Where($"{filter.Field} != {filter.Values[0]}");
    }

    private static void AddContainsFilter<T>(ref IQueryable<T> query, Filter filter) where T : BaseModel
    {
        query = query.Where($"{filter.Field}.Contains({filter.Values[0]})");
    }

    private static void AddArrayFilter<T>(ref IQueryable<T> query, Filter filter) where T : BaseModel
    {
        query = query.Where($"{filter.Field} IN ({string.Join(",", filter.Values)})");
    }

    private static void AddRangeFilter<T>(ref IQueryable<T> query, Filter filter) where T : BaseModel
    {
        query = query.Where($"{filter.Field} >= {filter.Values[0]} AND {filter.Field} <= {filter.Values[1]}");
    }
}
