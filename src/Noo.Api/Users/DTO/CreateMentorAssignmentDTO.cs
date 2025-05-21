using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Noo.Api.Users.DTO;

public record CreateMentorAssignmentDTO
{
    [Required]
    [JsonPropertyName("studentId")]
    public Ulid StudentId { get; set; }

    [Required]
    [JsonPropertyName("mentorId")]
    public Ulid MentorId { get; set; }

    [Required]
    [JsonPropertyName("subjectId")]
    public Ulid SubjectId { get; set; }
}
