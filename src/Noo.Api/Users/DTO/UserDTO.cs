using System.Text.Json.Serialization;
using Noo.Api.Core.Security.Authorization;

namespace Noo.Api.Users.DTO;

public record UserDTO
{
    public Ulid Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public UserRoles Role { get; set; } = UserRoles.Student;

    [JsonIgnore]
    public string PasswordHash { get; set; } = string.Empty;

    public bool IsBlocked { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
