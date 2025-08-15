using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Noo.Api.Core.Utils.Richtext;

namespace Noo.Api.Courses.DTO;

public record CreateCourseMaterialContentDTO
{
    [JsonPropertyName("content")]
    [Required]
    public IRichTextType Content { get; init; } = default!;

    [JsonPropertyName("workId")]
    public Ulid? WorkId { get; init; }

    [JsonPropertyName("isWorkAvailable")]
    [Required]
    public bool IsWorkAvailable { get; init; }

    [JsonPropertyName("workSolveDeadlineAt")]
    public DateTime? WorkSolveDeadlineAt { get; init; }

    [JsonPropertyName("workCheckDeadlineAt")]
    public DateTime? WorkCheckDeadlineAt { get; init; }
}
