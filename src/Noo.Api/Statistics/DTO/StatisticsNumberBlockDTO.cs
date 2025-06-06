using System.Text.Json.Serialization;
using Google.Apis.Sheets.v4.Data;

namespace Noo.Api.Statistics.DTO;

public record StatisticsNumberBlockDTO
{
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("value")]
    public string Value { get; init; } = string.Empty;

    [JsonPropertyName("units")]
    public string? Units { get; init; }

    [JsonPropertyName("subValues")]
    public Dictionary<string, double> SubValues { get; init; } = [];
}
