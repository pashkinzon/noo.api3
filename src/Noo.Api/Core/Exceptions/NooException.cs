using System.Net;
using Noo.Api.Core.Utils;

namespace Noo.Api.Core.Exceptions;

public class NooException : Exception
{
    public string Id { get; init; } = "UNKNOWN_ERROR";

    public string? LogId { get; init; }

    public bool IsInternal { get; init; } = false;

    public HttpStatusCode StatusCode { get; init; } = HttpStatusCode.InternalServerError;

    public object? Payload { get; set; }

    public NooException(string message = "Unknown error") : base(message) { }

    public NooException(HttpStatusCode statusCode, string message = "Unknown error") : base(message)
    {
        StatusCode = statusCode;
    }

    public object Serialize()
    {
        return new
        {
            id = Id,
            statusCode = StatusCode.GetHashCode(),
            message = Message,
            payload = Payload,
        };
    }

    public object SerializePublicly()
    {
        return new
        {
            id = Id,
            logId = LogId,
            statusCode = StatusCode.GetHashCode(),
            message = publicMessage(),
            payload = Payload,
        };
    }

    public static NooException FromUnhandled(Exception exception)
    {
        return new NooException(HttpStatusCode.InternalServerError, exception.Message)
        {
            LogId = RandomGenerator.GenerateReadableCode(),
            IsInternal = true
        };
    }

    private string publicMessage()
    {
        if (IsInternal)
        {
            return "An error occurred. Please try again later.";
        }

        return Message;
    }
}
