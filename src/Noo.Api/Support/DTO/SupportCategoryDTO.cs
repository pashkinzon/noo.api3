using System.Text.Json.Serialization;

namespace Noo.Api.Support.DTO;

public record SupportCategoryDTO
{
    [JsonPropertyName("id")]
    public Ulid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("isPinned")]
    public bool IsPinned { get; set; }

    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; }

    [JsonPropertyName("parentId")]
    public Ulid? ParentId { get; set; }

    [JsonPropertyName("children")]
    public IEnumerable<SupportCategoryDTO> Children { get; set; } = [];

    [JsonPropertyName("articles")]
    public IEnumerable<SupportArticleDTO> Articles { get; set; } = [];

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; }
}
