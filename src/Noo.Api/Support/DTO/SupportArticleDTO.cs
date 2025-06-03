using System.Text.Json.Serialization;
using Noo.Api.Core.Utils.Richtext;
using Noo.Api.Core.Validation.Attributes;

namespace Noo.Api.Support.DTO;

public record SupportArticleDTO
{
    [JsonPropertyName("id")]
    public Ulid Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("content")]
    [RichText(AllowEmpty = false, AllowNull = false)]
    public IRichTextType Content { get; set; } = default!;

    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; } = true;

    [JsonPropertyName("categoryId")]
    public Ulid CategoryId { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; }
}
