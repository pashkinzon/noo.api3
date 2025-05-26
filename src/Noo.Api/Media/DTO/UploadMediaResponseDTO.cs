using System.Text.Json.Serialization;

namespace Noo.Api.Media.DTO;

public record UploadMediaResponseDTO
{
    [JsonPropertyName("id")]
    public Ulid Id { get; init; }

    [JsonPropertyName("url")]
    public string Url { get; init; } = default!;
}
