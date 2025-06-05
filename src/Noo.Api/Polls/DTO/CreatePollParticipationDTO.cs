using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Noo.Api.Polls.Types;

namespace Noo.Api.Polls.DTO;

public record CreatePollParticipationDTO
{
    [JsonPropertyName("userType")]
    public ParticipatingUserType UserType { get; init; }

    [JsonPropertyName("userExternalIdentifier")]
    public string? UserExternalIdentifier { get; init; }

    [JsonPropertyName("userExternalData")]
    public object? UserExternalData { get; init; }

    [JsonPropertyName("answers")]
    [Required]
    public IEnumerable<CreatePollAnswerDTO> Answers { get; init; } = [];
}
