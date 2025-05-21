using System.Text.Json.Serialization;

namespace Noo.Api.Core.Response;

public record IdResponseDTO(
    [property: JsonPropertyName("id")]
    Ulid Id
);
