using System.Text.Json.Serialization;
using Noo.Api.Calendar.Types;

namespace Noo.Api.Calendar.DTO;

public record CreateCalendarEventDTO
{
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("type")]
    public CalendarEventType Type { get; init; }

    [JsonPropertyName("startDateTime")]
    public DateTime StartDateTime { get; init; }

    [JsonPropertyName("endDateTime")]
    public DateTime? EndDateTime { get; init; }

    [JsonPropertyName("assignedWorkId")]
    public Ulid? AssignedWorkId { get; init; }
}
