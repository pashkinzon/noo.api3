using System.ComponentModel.DataAnnotations;

namespace Noo.Api.Subjects.DTO;

public record SubjectCreationDTO
{
    [Required]
    [MinLength(1)]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [RegularExpression("^#[0-9A-Fa-f]{6}$", ErrorMessage = "Color must be a valid hex color code.")]
    public string Color { get; set; } = string.Empty;
}
