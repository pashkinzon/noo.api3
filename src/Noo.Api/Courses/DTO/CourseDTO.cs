using System.Text.Json.Serialization;

namespace Noo.Api.Courses.DTO;

public record CourseDTO
{
    [JsonPropertyName("id")]
    public Ulid Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("thumbnailId")]
    public Ulid? ThumbnailId { get; init; }
}
