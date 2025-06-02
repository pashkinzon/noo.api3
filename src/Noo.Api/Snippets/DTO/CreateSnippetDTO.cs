using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Noo.Api.Core.Utils.Richtext;
using Noo.Api.Core.Validation.Attributes;

namespace Noo.Api.Snippets.DTO;

public record CreateSnippetDTO
{
    [JsonPropertyName("name")]
    [MaxLength(63)]
    public string? Name { get; set; }

    [JsonPropertyName("content")]
    [RichText(AllowEmpty = false, AllowNull = false)]
    public IRichTextType? Content { get; set; }
}
