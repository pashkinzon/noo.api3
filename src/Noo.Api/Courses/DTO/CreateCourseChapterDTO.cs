using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Noo.Api.Courses.DTO;

public record CreateCourseChapterDTO
{
    [JsonPropertyName("title")]
    [Required]
    [MinLength(1)]
    [MaxLength(255)]
    public string Title { get; init; } = string.Empty;

    [JsonPropertyName("color")]
    [MaxLength(63)]
    public string? Color { get; set; }

    [JsonPropertyName("isActive")]
    [Required]
    public bool IsActive { get; init; }

    [JsonPropertyName("parentChapterId")]
    public Ulid? ParentChapterId { get; init; }
}
