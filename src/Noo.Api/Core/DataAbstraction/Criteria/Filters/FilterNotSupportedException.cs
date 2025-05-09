using System.Net;
using Noo.Api.Core.Exceptions;

namespace Noo.Api.Core.DataAbstraction.Criteria.Filters;

public class FilterNotSupportedException : NooException
{
    public FilterNotSupportedException(string filterString)
        : base($"Filter not supported: {filterString}")
    {
        Id = "CRITERIA_FILTER_NOT_SUPPORTED";
        StatusCode = HttpStatusCode.BadRequest;
    }

    public FilterNotSupportedException(string propertyName, FilterType type)
        : base($"Filter not supported: {propertyName} with type {type}")
    {
        Id = "CRITERIA_FILTER_NOT_SUPPORTED";
        StatusCode = HttpStatusCode.BadRequest;
    }
}
