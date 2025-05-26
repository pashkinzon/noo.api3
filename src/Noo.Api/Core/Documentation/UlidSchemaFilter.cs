using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Noo.Api.Core.Documentation;

public class UlidSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(Ulid))
        {
            schema.Type = "string";
            schema.Format = "ulid";
            schema.Example = new OpenApiString("01ARZ3NDEKTSV4RRFFQ69G5FAV");
            schema.Description = "Universally Unique Lexicographically Sortable Identifier (ULID).";
            schema.Pattern = "^[0-7][0-9A-HJKMNP-TV-Z]{25}$";
            schema.ExternalDocs = new OpenApiExternalDocs
            {
                Description = "ULID Specification",
                Url = new Uri("https://github.com/ulid/spec")
            };
        }
    }
}
