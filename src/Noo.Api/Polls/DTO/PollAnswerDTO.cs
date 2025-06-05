using System.Text.Json.Serialization;
using Noo.Api.Polls.Types;

namespace Noo.Api.Polls.DTO;

public record PollAnswerDTO
{
    [JsonPropertyName("id")]
    public Ulid Id { get; init; }

    [JsonPropertyName("pollQuestionId")]
    public Ulid PollQuestionId { get; init; }

    [JsonPropertyName("value")]
    public PollAnswerValue? Value { get; init; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; init; }
}
