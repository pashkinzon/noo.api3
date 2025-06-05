using System.Text.Json.Serialization;
using Noo.Api.Core.Utils.Richtext;
using Noo.Api.Core.Validation.Attributes;

namespace Noo.Api.AssignedWorks.DTO;

public record UpsertAssignedWorkCommentDTO
{
    [JsonPropertyName("id")]
    public Ulid? Id { get; set; }

    [JsonPropertyName("content")]
    [RichText(AllowEmpty = true, AllowNull = true)]
    public IRichTextType? Content { get; set; }
}
