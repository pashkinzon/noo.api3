using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Noo.Api.Core.Validation.Attributes;

namespace Noo.Api.Auth.DTO;

public record RegisterDTO
{
    [Required]
    [MinLength(1)]
    [MaxLength(200)]
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [Required]
    [Password]
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
}
