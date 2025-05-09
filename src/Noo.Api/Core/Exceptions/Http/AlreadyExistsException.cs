using System.Net;

namespace Noo.Api.Core.Exceptions.Http;

public class AlreadyExistsException : NooException
{
    public AlreadyExistsException(string message = "Already exists") : base(message)
    {
        Id = "ALREADY_EXISTS";
        StatusCode = HttpStatusCode.Conflict;
    }
}
