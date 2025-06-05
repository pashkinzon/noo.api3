using System.Text.Json.Serialization;

namespace Noo.Api.Polls.Types;

public struct PollAnswerValue
{
    [JsonPropertyName("type")]
    public PollQuestionType Type { get; set; }

    [JsonPropertyName("value")]
    // TODO: Add validation for specific types based on PollQuestionType
    public object? Value { get; set; }
}
