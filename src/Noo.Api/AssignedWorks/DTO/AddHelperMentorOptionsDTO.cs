using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Noo.Api.AssignedWorks.DTO;

public record AddHelperMentorOptionsDTO
{
    [JsonPropertyName("mentorId")]
    [Required]
    public Ulid MentorId { get; init; }
}
