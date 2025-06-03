using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Noo.Api.Support.DTO;

public record CreateSupportCategoryDTO
{
    [JsonPropertyName("name")]
    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("isPinned")]
    public bool IsPinned { get; set; }

    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; } = true;

    [JsonPropertyName("parentId")]
    public Ulid? ParentId { get; set; }
}
