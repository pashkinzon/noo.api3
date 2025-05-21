using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Noo.Api.Core.Validation.Attributes;

namespace Noo.Api.Users.DTO;

public record UpdatePasswordDTO
{
    [Required]
    [Password]
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}
