using System.ComponentModel.DataAnnotations;

namespace Noo.Api.Auth.DTO;

public record ConfirmEmailChangeDTO
{
    [Required]
    public string Token { get; init; } = null!;
}
