using System.Net;

namespace Noo.Api.Core.Exceptions.Http;

public class NotFoundException : NooException
{
    public NotFoundException(string message = "Not found") : base(message)
    {
        Id = "NOT_FOUND";
        StatusCode = HttpStatusCode.NotFound;
    }
}
