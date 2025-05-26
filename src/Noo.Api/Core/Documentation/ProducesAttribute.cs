using System.Net;
using Noo.Api.Core.Exceptions;

namespace Noo.Api.Core.Documentation;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
public class ProducesAttribute : Attribute
{
    public Type? ResponseType { get; init; } = null;
    public int Status { get; init; }
    public IEnumerable<int> ExceptionCodes { get; set; } = [];

    public ProducesAttribute(Type? responseType, int statusCode, params int[] exceptionCodes)
    {
        ResponseType = responseType;
        Status = statusCode;
        ExceptionCodes = exceptionCodes;
    }
}
