using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Noo.Api.Core.Validation.Attributes;

namespace Noo.Api.Auth.DTO;

public record LoginDTO
{
    [Required]
    [MinLength(1)]
    [MaxLength(200)]
    [JsonPropertyName("usernameOrEmail")]
    public string UsernameOrEmail { get; set; } = string.Empty;

    [Required]
    [Password]
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}
