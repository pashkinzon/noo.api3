using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Noo.Api.Polls.DTO;

/// <summary>
/// DTO to update a poll question.
/// </summary>
/// <remarks>The fields type and config cannot be changed after creation,
/// that's why they are not present here</remarks>
public record UpdatePollQuestionDTO
{
    [JsonPropertyName("title")]
    [MaxLength(255)]
    public string? Title { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    [MaxLength(512)]
    public string? Description { get; init; }

    [JsonPropertyName("isRequired")]
    public bool? IsRequired { get; init; }
}
