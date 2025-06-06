using System.Text.Json.Serialization;

namespace Noo.Api.Statistics.DTO;

public record StatisticsDTO
{
    [JsonPropertyName("blocks")]
    public IEnumerable<StatisticsBlockDTO> Blocks { get; init; } = [];
}
