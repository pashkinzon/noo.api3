using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Noo.Api.Polls.Types;

namespace Noo.Api.Polls.DTO;

public record CreatePollAnswerDTO
{
    [JsonPropertyName("pollQuestionId")]
    [Required]
    public Ulid PollQuestionId { get; init; }

    [JsonPropertyName("value")]
    public PollAnswerValue? Value { get; init; }
}
