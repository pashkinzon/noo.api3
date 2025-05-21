using System.Text.Json.Serialization;

namespace Noo.Api.Core.Response;

public record ApiResponseDTO<T>(
    [property: JsonPropertyName("data")]
    T? Data,

    [property: JsonPropertyName("meta")]
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    object? Metadata
);
