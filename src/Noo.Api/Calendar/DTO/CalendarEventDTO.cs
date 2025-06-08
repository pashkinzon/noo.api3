using System.Text.Json.Serialization;
using Noo.Api.Calendar.Types;

namespace Noo.Api.Calendar.DTO;

public record CalendarEventDTO
{
    [JsonPropertyName("id")]
    public Ulid Id { get; init; }

    [JsonPropertyName("assignedWorkId")]
    public Ulid? AssignedWorkId { get; init; }

    [JsonPropertyName("type")]
    public CalendarEventType Type { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("dateTime")]
    public DateTime DateTime { get; init; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; init; }
}
