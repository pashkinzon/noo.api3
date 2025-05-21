using System.Net;

namespace Noo.Api.Core.Exceptions.Http;

public class UserIsBlockedException : NooException
{
    public UserIsBlockedException(string message = "A user is blocked") : base(message)
    {
        Id = "USER_IS_BLOCKED";
        StatusCode = HttpStatusCode.Forbidden;
    }
}
