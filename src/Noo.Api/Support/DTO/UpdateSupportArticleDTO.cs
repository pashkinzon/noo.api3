using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Noo.Api.Core.Utils.Richtext;
using Noo.Api.Core.Validation.Attributes;

namespace Noo.Api.Support.DTO;

public record UpdateSupportArticleDTO
{
    [JsonPropertyName("title")]
    [MaxLength(255)]
    public string? Title { get; set; }

    [JsonPropertyName("content")]
    [RichText(AllowEmpty = false, AllowNull = true)]
    public IRichTextType? Content { get; set; }

    [JsonPropertyName("isActive")]
    public bool? IsActive { get; set; } = true;

    [JsonPropertyName("categoryId")]
    public Ulid? CategoryId { get; set; }
}
