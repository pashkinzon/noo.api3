using System.Text.Json.Serialization;
using Noo.Api.Core.Utils.Richtext;

namespace Noo.Api.Snippets.DTO;

public record SnippetDTO
{
    [JsonPropertyName("id")]
    public Ulid Id { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("content")]
    public IRichTextType? Content { get; init; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; init; }
}
