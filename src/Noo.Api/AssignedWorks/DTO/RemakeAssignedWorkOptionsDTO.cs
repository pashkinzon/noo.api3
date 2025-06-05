using System.Text.Json.Serialization;

namespace Noo.Api.AssignedWorks.DTO;

public record RemakeAssignedWorkOptionsDTO
{
    [JsonPropertyName("includeOnlyWrongTasks")]
    public bool IncludeOnlyWrongTasks { get; init; }
}
