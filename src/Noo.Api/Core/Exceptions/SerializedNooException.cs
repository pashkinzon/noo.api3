using System.Text.Json.Serialization;

namespace Noo.Api.Core.Exceptions;

public class SerializedNooException
{
    [JsonPropertyName("id")]
    public string Id { get; init; } = "UNKNOWN_ERROR";

    [JsonPropertyName("logId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? LogId { get; init; }

    [JsonPropertyName("statusCode")]
    public int StatusCode { get; init; } = 500;

    [JsonPropertyName("message")]
    public string Message { get; init; } = "Unknown error";

    [JsonPropertyName("payload")]
    public object? Payload { get; set; }
}
