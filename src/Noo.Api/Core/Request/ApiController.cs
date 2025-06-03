using Microsoft.AspNetCore.Mvc;
using Noo.Api.Core.Response;

namespace Noo.Api.Core.Request;

public abstract class ApiController : ControllerBase
{
    protected OkObjectResult OkResponse(object? data, object? metadata = null)
    {
        return Ok(new ApiResponseDTO<object?>(data, metadata));
    }

    protected OkObjectResult OkResponse((IEnumerable<object> data, int total) response)
    {
        return Ok(new ApiResponseDTO<object?>(response.data, new { response.total }));
    }

    protected CreatedResult CreatedResponse(Ulid id)
    {
        return Created(string.Empty, new ApiResponseDTO<object?>(new { Id = id }, null));
    }
}

