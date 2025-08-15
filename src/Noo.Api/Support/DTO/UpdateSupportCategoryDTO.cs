using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Noo.Api.Support.DTO;

public record UpdateSupportCategoryDTO
{
    [JsonPropertyName("name")]
    [MaxLength(255)]
    public string? Name { get; set; }

    [JsonPropertyName("order")]
    [Range(0, 255)]
    public int? Order { get; set; }

    [JsonPropertyName("isPinned")]
    public bool? IsPinned { get; set; }

    [JsonPropertyName("isActive")]
    public bool? IsActive { get; set; }

    [JsonPropertyName("parentId")]
    public Ulid? ParentId { get; set; }
}
