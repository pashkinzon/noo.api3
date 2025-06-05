using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Noo.Api.AssignedWorks.DTO;

public record ShiftAssignedWorkDeadlineOptionsDTO
{
    [JsonPropertyName("newDeadline")]
    [Required]
    public DateTime NewDeadline { get; init; }

    [JsonPropertyName("notifyOthers")]
    public bool NotifyOthers { get; init; }
}
