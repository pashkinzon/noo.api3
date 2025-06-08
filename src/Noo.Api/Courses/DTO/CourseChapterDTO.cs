using System.Text.Json.Serialization;

namespace Noo.Api.Courses.DTO;

public record CourseChapterDTO
{
    [JsonPropertyName("id")]
    public Ulid Id { get; init; }

    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    [JsonPropertyName("color")]
    public string Color { get; set; } = string.Empty;

    [JsonPropertyName("isActive")]
    public bool IsActive { get; init; }

    [JsonPropertyName("parentChapterId")]
    public Ulid? ParentChapterId { get; init; }

    [JsonPropertyName("subChapters")]
    public IEnumerable<CourseChapterDTO> SubChapters { get; init; } = [];

    [JsonPropertyName("materials")]
    public IEnumerable<CourseMaterialDTO> Materials { get; init; } = [];
}
