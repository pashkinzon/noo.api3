using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Noo.Api.Core.Exceptions.Http;
using Noo.Api.Core.Response;
using Noo.Api.Core.Utils.DI;

namespace Noo.Api.Core.Exceptions;

[RegisterSingleton(typeof(IClientErrorFactory))]
public class NooExceptionErrorFactory : IClientErrorFactory
{
    public IActionResult GetClientError(ActionContext actionContext, IClientErrorActionResult clientError)
    {
        var statusCode = clientError.StatusCode ?? StatusCodes.Status400BadRequest;
        var errorResponse = clientError.GetType() == typeof(NooException)
            ? new ErrorApiResponseDTO(((NooException)clientError).SerializePublicly())
            : new ErrorApiResponseDTO(GetError(clientError, statusCode).SerializePublicly());

        return new ObjectResult(errorResponse)
        {
            StatusCode = statusCode
        };
    }

    private static NooException GetError(IClientErrorActionResult clientError, int statusCode)
    {
        switch (statusCode)
        {
            case StatusCodes.Status400BadRequest:
                return new BadRequestException();
            case StatusCodes.Status401Unauthorized:
                return new UnauthorizedException();
            case StatusCodes.Status403Forbidden:
                return new ForbiddenException();
            case StatusCodes.Status404NotFound:
                return new NotFoundException();
            case StatusCodes.Status409Conflict:
                return new AlreadyExistsException();
            case StatusCodes.Status415UnsupportedMediaType:
                return new UnsupportedMediaTypeException();
            default:
                throw new Exception($"Unhandled client error: {clientError.GetType().Name}");
        }
    }
}
