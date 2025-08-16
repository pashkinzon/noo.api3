using System.Text.Json.Serialization;
using Noo.Api.Notifications.Types;

namespace Noo.Api.Notifications.DTO;

public record BulkCreateNotificationsDTO
{
    [JsonPropertyName("userIds")]
    public IEnumerable<Ulid> UserIds { get; init; } = Array.Empty<Ulid>();

    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    [JsonPropertyName("message")]
    public string Message { get; init; } = string.Empty;

    [JsonPropertyName("isBanner")]
    public bool IsBanner { get; init; }

    [JsonPropertyName("link")]
    public string? Link { get; init; }

    [JsonPropertyName("linkText")]
    public string? LinkText { get; init; }

    [JsonPropertyName("channels")]
    public IEnumerable<NotificationChannelType>? Channels { get; init; }
}
