using System.Text.Json.Serialization;
using Noo.Api.Media.DTO;
using Noo.Api.NooTube.Types;
using Noo.Api.Users.DTO;

namespace Noo.Api.NooTube.DTO;

public record NooTubeVideoDTO
{
    [JsonPropertyName("id")]
    public Ulid Id { get; init; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("thumbnailId")]
    public Ulid? ThumbnailId { get; set; }

    [JsonPropertyName("externalIdentifier")]
    public string? ExternalIdentifier { get; set; }

    [JsonPropertyName("externalUrl")]
    public string? ExternalUrl { get; set; }

    [JsonPropertyName("externalThumbnailUrl")]
    public string? ExternalThumbnailUrl { get; set; }

    [JsonPropertyName("serviceType")]
    public NooTubeServiceType ServiceType { get; set; }

    [JsonPropertyName("state")]
    public VideoState State { get; set; } = VideoState.NotUploaded;

    [JsonPropertyName("duration")]
    public int? Duration { get; set; }

    [JsonPropertyName("publishedAt")]
    public DateTime PublishedAt { get; set; }

    [JsonPropertyName("uploadedById")]
    public Ulid UploadedById { get; set; }

    [JsonPropertyName("uploadedBy")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public UserDTO? UploadedBy { get; set; }

    [JsonPropertyName("thumbnail")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public MediaDTO? Thumbnail { get; set; }

    [JsonPropertyName("comments")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<NooTubeVideoCommentDTO>? Comments { get; set; }
}
