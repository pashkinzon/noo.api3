using System.Text.Json.Serialization;

namespace Noo.Api.Core.Response;

public record ApiResponseDTO(
    [property: JsonPropertyName("data")]
    object? Data,

    [property: JsonPropertyName("meta")]
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    object? Metadata
);
