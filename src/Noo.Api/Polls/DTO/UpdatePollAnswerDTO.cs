using System.Text.Json.Serialization;
using Noo.Api.Polls.Types;

namespace Noo.Api.Polls.DTO;

public record UpdatePollAnswerDTO
{
    [JsonPropertyName("value")]
    public PollAnswerValue Value { get; init; }
}
