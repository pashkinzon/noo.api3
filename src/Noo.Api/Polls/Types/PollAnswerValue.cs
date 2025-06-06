using System.Data.Entity.Infrastructure;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Noo.Api.Polls.Types;

public struct PollAnswerValue
{
    [JsonPropertyName("type")]
    public PollQuestionType Type { get; set; }

    [JsonPropertyName("value")]
    public object? Value { get; set; }

    public readonly string Serialize()
    {
        return JsonSerializer.Serialize(this);
    }

    public static PollAnswerValue Deserialize(string raw)
    {
        return JsonSerializer.Deserialize<PollAnswerValue>(raw);
    }
}
