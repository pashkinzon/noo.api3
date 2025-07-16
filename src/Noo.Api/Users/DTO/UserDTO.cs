using System.Text.Json.Serialization;
using Noo.Api.Core.Security.Authorization;

namespace Noo.Api.Users.DTO;

public record UserDTO
{
    [JsonPropertyName("id")]
    public Ulid Id { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("telegramId")]
    public string? TelegramId { get; set; }

    [JsonPropertyName("telegramUsername")]
    public string? TelegramUsername { get; set; }

    [JsonPropertyName("role")]
    public UserRoles Role { get; set; } = UserRoles.Student;

    [JsonIgnore]
    public string PasswordHash { get; set; } = string.Empty;

    [JsonPropertyName("isBlocked")]
    public bool IsBlocked { get; set; }

    [JsonPropertyName("isVerified")]
    public bool IsVerified { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTime? UpdatedAt { get; set; }
}
