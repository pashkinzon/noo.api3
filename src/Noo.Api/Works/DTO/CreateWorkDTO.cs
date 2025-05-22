using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using Noo.Api.Works.Types;

namespace Noo.Api.Works.DTO;

public record CreateWorkDTO
{
    [Required]
    [MinLength(1)]
    [MaxLength(200)]
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [JsonPropertyName("type")]
    public WorkType Type { get; set; }

    [MaxLength(255)]
    public string? Description { get; set; }

    [Required]
    [JsonPropertyName("subjectId")]
    public Ulid? SubjectId { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(300)]
    [ValidateEnumeratedItems]
    [JsonPropertyName("tasks")]
    public ICollection<CreateWorkTaskDTO> Tasks { get; set; } = [];
}
