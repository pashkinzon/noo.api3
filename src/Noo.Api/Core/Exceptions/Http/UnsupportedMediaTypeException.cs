using System.Net;

namespace Noo.Api.Core.Exceptions.Http;

public class UnsupportedMediaTypeException : NooException
{
    public UnsupportedMediaTypeException(string message = "Unsupported media type") : base(message)
    {
        Id = "UNSUPPORTED_MEDIA_TYPE";
        StatusCode = HttpStatusCode.UnsupportedMediaType;
    }
}
