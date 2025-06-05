using System.Text.Json.Serialization;
using Noo.Api.UserSettings.Types;

namespace Noo.Api.UserSettings.DTO;

public record UserSettingsDTO
{
    [JsonPropertyName("theme")]
    public UserTheme? Theme { get; init; }

    [JsonPropertyName("fontSize")]
    public FontSize? FontSize { get; init; }

    // TODO: Add media as background image
}
