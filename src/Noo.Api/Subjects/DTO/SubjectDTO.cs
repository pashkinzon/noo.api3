namespace Noo.Api.Subjects.DTO;

public record SubjectDTO
{
    public Ulid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Color { get; set; } = string.Empty;
}
