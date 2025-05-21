using System.Net;

namespace Noo.Api.Core.Exceptions.Http;

public class UserIsNotVerifiedException : NooException
{
    public UserIsNotVerifiedException(string message = "User is not verified") : base(message)
    {
        Id = "USER_IS_NOT_VERIFIED";
        StatusCode = HttpStatusCode.Unauthorized;
    }
}
