using System.Net;
using Noo.Api.Core.Exceptions;

namespace Noo.Api.Core.DataAbstraction.Criteria;

public class SortNotSupportedException : NooException
{
    public SortNotSupportedException(string sort) : base($"Sort by {sort} is not supported in the given entity.")
    {
        Id = "SORT_NOT_SUPPORTED";
        StatusCode = HttpStatusCode.BadRequest;
    }
}
