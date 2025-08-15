using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Noo.Api.GoogleSheetsIntegrations.Types;

namespace Noo.Api.GoogleSheetsIntegrations.DTO;

public record CreateGoogleSheetsIntegrationDTO
{
    [JsonPropertyName("name")]
    [Required]
    [MinLength(1)]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("entity")]
    [Required]
    [MinLength(1)]
    [MaxLength(255)]
    public string Entity { get; set; } = string.Empty;

    [JsonPropertyName("cronPattern")]
    [Required]
    public string CronPattern { get; set; } = string.Empty;

    [JsonPropertyName("googleAuthData")]
    [Required]
    public GoogleSheetsAuthData GoogleAuthData { get; set; } = default!;
}
