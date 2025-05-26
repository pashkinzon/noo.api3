using Microsoft.OpenApi.Models;
using Noo.Api.Core.Documentation;
using Noo.Api.Core.Exceptions;
using Swashbuckle.AspNetCore.SwaggerGen;

public class ProducesOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var attributes = context.MethodInfo
            .GetCustomAttributes(typeof(ProducesAttribute), false)
            .Cast<ProducesAttribute>();

        foreach (var attr in attributes)
        {
            // Add the primary response (success case)
            AddResponse(operation, attr.Status, attr.ResponseType);

            // Add error responses from exception types
            foreach (var statusCode in attr.ExceptionCodes)
            {
                AddResponse(operation, statusCode, typeof(SerializedNooException));
            }
        }
    }

    private void AddResponse(OpenApiOperation operation, int statusCode, Type? responseType)
    {
        var statusCodeStr = statusCode.ToString();
        var response = new OpenApiResponse { Description = "Result" };

        if (responseType != null)
        {
            response.Content = new Dictionary<string, OpenApiMediaType>
            {
                ["application/json"] = new OpenApiMediaType
                {
                    Schema = new OpenApiSchema { Reference = new OpenApiReference { Id = responseType.Name, Type = ReferenceType.Schema } }
                }
            };
        }

        if (!operation.Responses.ContainsKey(statusCodeStr))
        {
            operation.Responses.Add(statusCodeStr, response);
        }
    }
}
