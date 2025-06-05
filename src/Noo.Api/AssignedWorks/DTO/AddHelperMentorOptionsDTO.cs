using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Noo.Api.AssignedWorks.DTO;

public record AddHelperMentorOptionsDTO
{
    [JsonPropertyName("mentorId")]
    [Required]
    public Ulid MentorId { get; init; }

    [JsonPropertyName("notifyMentor")]
    public bool NotifyMentor { get; init; }

    [JsonPropertyName("notifyStudent")]
    public bool NotifyStudent { get; init; }
}
