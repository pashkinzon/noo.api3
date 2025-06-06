using System.Text.Json.Serialization;

namespace Noo.Api.Statistics.DTO;

public record StatisticsBlockDTO
{
    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("graph")]
    public StatisticsGraphDTO? Graph { get; init; }

    [JsonPropertyName("numberBlocks")]
    public IEnumerable<StatisticsNumberBlockDTO> NumberBlocks { get; init; } = [];
}
