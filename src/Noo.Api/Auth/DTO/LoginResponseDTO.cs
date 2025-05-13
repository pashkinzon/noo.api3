using System.Text.Json.Serialization;

namespace Noo.Api.Auth.DTO;

public record LoginResponseDTO
{
    [JsonPropertyName("accessToken")]
    public string AccessToken { get; set; } = string.Empty;

    [JsonPropertyName("expiresIn")]
    public DateTime ExpiresAt { get; set; }

    [JsonPropertyName("userInfo")]
    public UserInfoDTO UserInfo { get; set; } = new();
}
