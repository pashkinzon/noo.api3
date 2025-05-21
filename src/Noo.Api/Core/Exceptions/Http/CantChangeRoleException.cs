using System.Net;

namespace Noo.Api.Core.Exceptions.Http;

public class CantChangeRoleException : NooException
{
    public CantChangeRoleException(string message = "Role change is not possible") : base(message)
    {
        Id = "CANT_CHANGE_ROLE";
        StatusCode = HttpStatusCode.Forbidden;
    }
}
