using System.Text.Json.Serialization;

namespace Noo.Api.Calendar.DTO;

public record CreateCalendarEventDTO
{
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("dateTime")]
    public DateTime DateTime { get; init; }
}
