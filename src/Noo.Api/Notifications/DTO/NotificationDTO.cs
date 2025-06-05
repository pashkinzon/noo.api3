using System.Text.Json.Serialization;

namespace Noo.Api.Notifications.DTO;

public record NotificationDTO
{
    [JsonPropertyName("id")]
    public Ulid Id { get; init; }

    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    [JsonPropertyName("message")]
    public string Message { get; init; } = string.Empty;

    [JsonPropertyName("is_read")]
    public bool IsRead { get; init; }

    [JsonPropertyName("is_banner")]
    public bool IsBanner { get; init; }

    [JsonPropertyName("link")]
    public string? Link { get; init; }

    [JsonPropertyName("link_text")]
    public string? LinkText { get; init; }
}
