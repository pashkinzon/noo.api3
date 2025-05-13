using System.Text.Json.Serialization;
using Noo.Api.Core.Security.Authorization;

namespace Noo.Api.Auth.DTO;

public record UserInfoDTO
{
    [JsonPropertyName("id")]
    public Ulid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("role")]
    public UserRoles Role { get; set; } = UserRoles.Student;
}
