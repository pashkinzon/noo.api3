using System.Text.Json.Serialization;

namespace Noo.Api.Works.DTO;

public record WorkResponseDTO
{
    [JsonPropertyName("id")]
    public Ulid Id { get; init; }

    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("tasks")]
    public ICollection<WorkTaskResponseDTO> Tasks { get; init; } = [];
}
