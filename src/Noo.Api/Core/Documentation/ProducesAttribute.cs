namespace Noo.Api.Core.Documentation;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
public class ProducesAttribute : Attribute
{
    public Type? ResponseType { get; init; }
    public int Status { get; init; }
    public IEnumerable<int> ExceptionCodes { get; init; } = [];

    public ProducesAttribute(Type? responseType, int statusCode, params int[] exceptionCodes)
    {
        ResponseType = responseType;
        Status = statusCode;
        ExceptionCodes = exceptionCodes;
    }
}
