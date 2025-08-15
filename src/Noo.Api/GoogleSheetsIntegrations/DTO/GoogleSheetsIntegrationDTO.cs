using System.Text.Json.Serialization;
using Noo.Api.GoogleSheetsIntegrations.Types;

namespace Noo.Api.GoogleSheetsIntegrations.DTO;

public record GoogleSheetsIntegrationDTO
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("entity")]
    public string Entity { get; set; } = string.Empty;

    [JsonPropertyName("lastRunAt")]
    public DateTime? LastRunAt { get; set; }

    [JsonPropertyName("status")]
    public GoogleSheetsIntegrationStatus Status { get; set; } = GoogleSheetsIntegrationStatus.Active;

    [JsonPropertyName("lastErrorText")]
    public string? LastErrorText { get; set; }

    [JsonPropertyName("cronPattern")]
    public string CronPattern { get; set; } = string.Empty;

    [JsonPropertyName("googleAuthData")]
    public GoogleSheetsAuthData GoogleAuthData { get; set; } = default!;

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTime? UpdatedAt { get; set; }
}
