using System.Text.Json.Serialization;
using Noo.Api.Subjects.DTO;

namespace Noo.Api.Works.DTO;

public record WorkDTO
{
    [JsonPropertyName("id")]
    public Ulid Id { get; init; }

    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("subjectId")]
    public Ulid? SubjectId { get; init; }

    [JsonPropertyName("subject")]
    public SubjectDTO? Subject { get; init; }

    [JsonPropertyName("tasks")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ICollection<WorkTaskDTO> Tasks { get; init; } = [];
}
