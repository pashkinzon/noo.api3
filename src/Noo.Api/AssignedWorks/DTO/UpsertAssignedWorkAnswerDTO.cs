using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Noo.Api.AssignedWorks.Types;
using Noo.Api.Core.Utils.Richtext;
using Noo.Api.Core.Validation.Attributes;

namespace Noo.Api.AssignedWorks.DTO;

public record UpsertAssignedWorkAnswerDTO
{
    [JsonPropertyName("id")]
    public Ulid? Id { get; set; }

    [JsonPropertyName("richTextContent")]
    [RichText(AllowEmpty = true, AllowNull = true)]
    public IRichTextType? RichTextContent { get; set; }

    [JsonPropertyName("wordContent")]
    [MaxLength(63)]
    public string? WordContent { get; set; }

    [JsonPropertyName("mentorComment")]
    [RichText(AllowEmpty = true, AllowNull = true)]
    public IRichTextType? MentorComment { get; set; }

    [JsonPropertyName("score")]
    [Range(0, 500)]
    public int? Score { get; set; }

    [JsonPropertyName("maxScore")]
    [Range(0, 500)]
    public int MaxScore { get; set; }

    [JsonPropertyName("detailedScore")]
    public Dictionary<string, int>? DetailedScore { get; set; }

    [JsonPropertyName("status")]
    [Required]
    public AssignedWorkAnswerStatus Status { get; set; } = AssignedWorkAnswerStatus.NotSubmitted;

    [JsonPropertyName("taskId")]
    [Required]
    public Ulid TaskId { get; set; }
}
