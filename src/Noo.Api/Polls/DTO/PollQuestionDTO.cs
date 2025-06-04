using System.Text.Json.Serialization;
using Noo.Api.Polls.Types;

namespace Noo.Api.Polls.DTO;

public record PollQuestionDTO
{
    [JsonPropertyName("id")]
    public Ulid Id { get; init; }

    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("isRequired")]
    public bool IsRequired { get; init; }

    [JsonPropertyName("type")]
    public PollQuestionType Type { get; init; }

    [JsonPropertyName("config")]
    public PollQuestionConfig? Config { get; init; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; init; }
}
