using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using SystemTextJsonPatch;

namespace Noo.Api.Core.Documentation;

public class JsonPatchOperationFilter : IOperationFilter
{
    /// <summary>
    /// If the type of the request body is a JSON Patch document,
    /// this operation filter will add the necessary metadata
    /// to the OpenAPI operation to indicate that it supports JSON Patch.
    /// </summary>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var patchParameter = context.MethodInfo
            .GetParameters()
            .FirstOrDefault(p => p.ParameterType.IsGenericType &&
                                 p.ParameterType.GetGenericTypeDefinition() == typeof(JsonPatchDocument<>));

        if (patchParameter != null)
        {
            operation.RequestBody = new OpenApiRequestBody
            {
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json-patch+json"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "array",
                            Items = new OpenApiSchema
                            {
                                Type = "object",
                                Properties = new Dictionary<string, OpenApiSchema>
                                {
                                    ["op"] = new OpenApiSchema { Type = "string" },
                                    ["path"] = new OpenApiSchema { Type = "string" },
                                    ["value"] = new OpenApiSchema { Type = "object" }
                                },
                                Required = new HashSet<string> { "op", "path" }
                            }
                        }
                    }
                }
            };
        }
    }
}
