using System.Text.Json.Serialization;
using Noo.Api.AssignedWorks.Types;

namespace Noo.Api.AssignedWorks.DTO;

public record AssignedWorkProgressDTO
{
    [JsonPropertyName("id")]
    public Ulid Id { get; init; }

    [JsonPropertyName("solve_status")]
    public AssignedWorkSolveStatus? SolveStatus { get; init; }

    [JsonPropertyName("solved_at")]
    public DateTime? SolvedAt { get; init; }

    [JsonPropertyName("check_status")]
    public AssignedWorkCheckStatus? CheckStatus { get; init; }

    [JsonPropertyName("checked_at")]
    public DateTime? CheckedAt { get; init; }

    [JsonPropertyName("score")]
    public int? Score { get; init; }

    [JsonPropertyName("max_score")]
    public int? MaxScore { get; init; }
}
