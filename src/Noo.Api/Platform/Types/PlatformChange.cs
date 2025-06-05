using System.Text.Json.Serialization;

namespace Noo.Api.Platform.Types;

public struct PlatformChange
{
    [JsonPropertyName("type")]
    public ChangeType Type { get; set; }

    [JsonPropertyName("author")]
    public string Author { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }
}
