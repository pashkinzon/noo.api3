using System.Net;

namespace Noo.Api.Core.Exceptions.Http;

public class UnauthorizedException : NooException
{
    public UnauthorizedException(string message = "Unauthorized") : base(message)
    {
        Id = "UNAUTHORIZED";
        StatusCode = HttpStatusCode.Unauthorized;
    }
}
