using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Noo.Api.Core.Exceptions.Http;
using Noo.Api.Core.Response;
using Noo.Api.Core.Utils.DI;

namespace Noo.Api.Core.Exceptions;

[RegisterScoped]
public class PipelineExceptionHandler
{
    private readonly ILogger<PipelineExceptionHandler> _logger;

    public PipelineExceptionHandler(ILogger<PipelineExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async Task HandleExceptionAsync(HttpContext context, Exception? exception = null)
    {
        if (context.Response.HasStarted)
        {
            return;
        }

        if (exception == null)
        {
            exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

            if (exception == null)
            {
                return;
            }
        }

        var error = exception switch
        {
            NooException nooException => nooException,
            _ => NooException.FromUnhandled(exception)
        };

        if (error.IsInternal)
        {
            _logger.LogError(exception, "Unhandled exception occurred. LogId: {LogId}", error.LogId);
        }

        var response = new ErrorApiResponseDTO(error.SerializePublicly());

        context.Response.StatusCode = (int)error.StatusCode;
        await context.Response.WriteAsJsonAsync(response);
    }

    public async Task HandleExceptionAsync(ExceptionContext context)
    {
        await HandleExceptionAsync(context.HttpContext, context.Exception);
        context.ExceptionHandled = true;
    }

    internal IActionResult HandleBadRequestException(ActionContext context)
    {
        var errors = context.ModelState
            .Where(e => e.Value?.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value!.Errors.Select(e => e.Exception?.Message ?? e.ErrorMessage).ToArray()
            );

        var error = new BadRequestException() { Payload = errors };
        var response = new ErrorApiResponseDTO(error.SerializePublicly());

        return new BadRequestObjectResult(response);
    }
}
