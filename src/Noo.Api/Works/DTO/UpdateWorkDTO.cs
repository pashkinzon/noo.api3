using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using Noo.Api.Works.Types;

namespace Noo.Api.Works.DTO;

public record UpdateWorkDTO
{
    [JsonPropertyName("id")]
    public Ulid? Id { get; set; }

    [MinLength(1)]
    [MaxLength(200)]
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [Required]
    [JsonPropertyName("type")]
    public WorkType? Type { get; set; }

    [MaxLength(255)]
    public string? Description { get; set; }

    [Required]
    [MaxLength(300)]
    [ValidateEnumeratedItems]
    public ICollection<UpdateWorkTaskDTO>? Tasks { get; set; }
}
