using System.Net;
using Noo.Api.Core.Exceptions;

namespace Noo.Api.Auth.Exceptions;

public class TokenExpiredException : NooException
{
    public TokenExpiredException() : base("Token has expired.")
    {
        Id = "TOKEN_EXPIRED";
        StatusCode = HttpStatusCode.RequestedRangeNotSatisfiable;
    }
}
