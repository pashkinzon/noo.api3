using System.Text.Json.Serialization;

namespace Noo.Api.Core.Response;

public record ErrorApiResponseDTO(
    [property: JsonPropertyName("error")]
    object? Error
);
