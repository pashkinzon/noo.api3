using System.Net;

namespace Noo.Api.Core.Exceptions.Http;

public class BadRequestException : NooException
{
    public BadRequestException(string message = "Bad request") : base(message)
    {
        Id = "BAD_REQUEST";
        StatusCode = HttpStatusCode.BadRequest;
    }
}
