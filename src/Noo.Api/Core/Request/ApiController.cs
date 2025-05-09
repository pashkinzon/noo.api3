using Microsoft.AspNetCore.Mvc;
using Noo.Api.Core.Response;

namespace Noo.Api.Core.Request;

public abstract class ApiController : ControllerBase
{
    protected OkObjectResult OkResponse(object? data, object? metadata = null)
    {
        Console.WriteLine("OkResponse called with object object");
        return Ok(new ApiResponseDTO(data, metadata));
    }

    protected OkObjectResult OkResponse((IEnumerable<object> data, int total) response)
    {
        Console.WriteLine("OkResponse called with tuple");
        return Ok(new ApiResponseDTO(response.data, new { response.total }));
    }

    protected CreatedResult CreatedResponse(Ulid id)
    {
        return Created(string.Empty, new ApiResponseDTO(new { Id = id }, null));
    }
}

