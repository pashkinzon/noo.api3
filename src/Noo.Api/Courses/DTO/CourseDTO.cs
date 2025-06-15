using System.Text.Json.Serialization;
using Noo.Api.Media.DTO;
using Noo.Api.Subjects.DTO;

namespace Noo.Api.Courses.DTO;

public record CourseDTO
{
    [JsonPropertyName("id")]
    public Ulid Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("startDate")]
    public DateTime StartDate { get; set; }

    [JsonPropertyName("endDate")]
    public DateTime EndDate { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("thumbnailId")]
    public Ulid? ThumbnailId { get; init; }

    [JsonPropertyName("thumbnail")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public MediaDTO? Thumbnail { get; set; }

    [JsonPropertyName("memberCount")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? MemberCount { get; init; }

    [JsonPropertyName("subjectId")]
    public Ulid SubjectId { get; init; }

    [JsonPropertyName("subject")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public SubjectDTO? Subject { get; init; }

    [JsonPropertyName("chapters")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<CourseChapterDTO> Chapters = [];
}
