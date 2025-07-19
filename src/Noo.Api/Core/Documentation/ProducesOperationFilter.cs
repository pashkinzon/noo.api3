using Microsoft.AspNetCore.WebUtilities;
using Microsoft.OpenApi.Models;
using Noo.Api.Core.Exceptions;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Noo.Api.Core.Documentation;

public class ProducesOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var attributes = context.MethodInfo
            .GetCustomAttributes(typeof(ProducesAttribute), false)
            .Cast<ProducesAttribute>();

        operation.Responses.Clear();

        foreach (var attr in attributes)
        {
            var schema = attr.ResponseType != null
                ? context.SchemaGenerator.GenerateSchema(attr.ResponseType, context.SchemaRepository)
                : null;

            AddResponse(operation, attr.Status, schema);

            var errorSchema = context.SchemaGenerator.GenerateSchema(typeof(SerializedNooException), context.SchemaRepository);

            // Add error responses from exception types
            foreach (var statusCode in attr.ExceptionCodes)
            {
                AddResponse(operation, statusCode, errorSchema);
            }
        }
    }

    private void AddResponse(OpenApiOperation operation, int statusCode, OpenApiSchema? schema)
    {
        var statusCodeStr = statusCode.ToString();
        var response = new OpenApiResponse
        {
            Description = GetDescription(statusCode),
            Content = new Dictionary<string, OpenApiMediaType>
            {
                ["application/json"] = new OpenApiMediaType
                {
                    Schema = schema
                }
            }
        };

        if (!operation.Responses.ContainsKey(statusCodeStr))
        {
            operation.Responses.Add(statusCodeStr, response);
        }
    }

    private string GetDescription(int statusCode)
    {
        return ReasonPhrases.GetReasonPhrase(statusCode);
    }
}
