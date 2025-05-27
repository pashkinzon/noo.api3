using System.Text.Json.Serialization;
using Noo.Api.Courses.Types;
using Noo.Api.Users.DTO;

namespace Noo.Api.Courses.DTO;

public record CourseMembershipDTO
{
    [JsonPropertyName("id")]
    public Ulid Id { get; init; }

    [JsonPropertyName("type")]
    public CourseMembershipType Type { get; init; }

    [JsonPropertyName("courseId")]
    public Ulid CourseId { get; init; }

    [JsonPropertyName("course")]
    public CourseDTO? Course { get; init; }

    [JsonPropertyName("isActive")]
    public bool IsActive { get; init; }

    [JsonPropertyName("isArchived")]
    public bool IsArchived { get; init; }

    [JsonPropertyName("studentId")]
    public Ulid StudentId { get; init; }

    [JsonPropertyName("student")]
    public UserDTO? Student { get; init; }

    [JsonPropertyName("assignerId")]
    public Ulid? AssignerId { get; init; }

    [JsonPropertyName("assigner")]
    public UserDTO? Assigner { get; init; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; init; }
}
