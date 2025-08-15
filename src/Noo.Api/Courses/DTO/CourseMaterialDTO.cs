using System.Text.Json.Serialization;

namespace Noo.Api.Courses.DTO;

public record CourseMaterialDTO
{
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    [JsonPropertyName("titleColor")]
    public string TItleColor { get; init; } = string.Empty;

    [JsonPropertyName("isActive")]
    public bool IsActive { get; init; }

    [JsonPropertyName("publishAt")]
    public DateTime PublishAt { get; init; }

    [JsonPropertyName("chapterId")]
    public Ulid ChapterId { get; init; }

    [JsonPropertyName("contentId")]
    public Ulid ContentId { get; init; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("updatedAt")]
    public DateTime? UpdatedAt { get; init; }
}
