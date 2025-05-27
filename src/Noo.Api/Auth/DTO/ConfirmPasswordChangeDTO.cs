using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Noo.Api.Core.Validation.Attributes;

namespace Noo.Api.Auth.DTO;

public record ConfirmPasswordChangeDTO
{
    [Required]
    [MinLength(1)]
    [MaxLength(200)]
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;

    [Required]
    [Password]
    [JsonPropertyName("newPassword")]
    public string NewPassword { get; set; } = string.Empty;
}
