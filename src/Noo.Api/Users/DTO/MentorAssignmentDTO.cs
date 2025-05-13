using Noo.Api.Subjects.DTO;

namespace Noo.Api.Users.DTO;

public record MentorAssignmentDTO
{
    public Ulid Id { get; set; }

    public Ulid StudentId { get; set; }

    public UserDTO? Student { get; set; }

    public Ulid MentorId { get; set; }

    public UserDTO? Mentor { get; set; }

    public Ulid SubjectId { get; set; }

    public SubjectDTO? Subject { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
