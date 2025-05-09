using System.Net;

namespace Noo.Api.Core.Exceptions.Http;

public class ForbiddenException : NooException
{
    public ForbiddenException(string message = "Forbidden") : base(message)
    {
        Id = "FORBIDDEN";
        StatusCode = HttpStatusCode.Forbidden;
    }
}
