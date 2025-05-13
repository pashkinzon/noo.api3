using System.ComponentModel.DataAnnotations;

namespace Noo.Api.Auth.DTO;

public record RequestEmailChangeDTO
{
    [Required]
    public string NewEmail { get; init; } = null!;
}
