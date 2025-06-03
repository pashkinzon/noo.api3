using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Noo.Api.Core.Utils.Richtext;
using Noo.Api.Core.Validation.Attributes;

namespace Noo.Api.Support.DTO;

public record CreateSupportArticleDTO
{
    [JsonPropertyName("title")]
    [Required]
    [MaxLength(255)]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("content")]
    [RichText(AllowEmpty = false, AllowNull = false)]
    public IRichTextType Content { get; set; } = default!;

    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; } = true;

    [JsonPropertyName("categoryId")]
    public Ulid CategoryId { get; set; }
}
