using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Noo.Api.Core.DataAbstraction.Model;
using Noo.Api.Core.DataAbstraction.Model.Attributes;
using Noo.Api.Core.Security.Authorization;

namespace Noo.Api.Users.Models;

[Model("user")]
public class UserModel : BaseModel
{
    [Required]
    [MinLength(1)]
    [MaxLength(200)]
    [Column("name", TypeName = "VARCHAR(200)")]
    [Index("user_name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MinLength(1)]
    [MaxLength(200)]
    [Column("username", TypeName = "VARCHAR(200)")]
    [Index("user_username_unique", IsUnique = true)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [Column("email", TypeName = "VARCHAR(200)")]
    [Index("user_email_unique", IsUnique = true)]
    public string Email { get; set; } = string.Empty;

    [Column("telegram_id", TypeName = "VARCHAR(200)")]
    [Index("user_telegram_id_unique", IsUnique = true)]
    public string? TelegramId { get; set; }

    [Column("telegram_username", TypeName = "VARCHAR(255)")]
    public string? TelegramUsername { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(255)]
    [Column("password_hash", TypeName = "VARCHAR(255)")]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    [Column("role", TypeName = "ENUM('student', 'mentor', 'assistant', 'teacher', 'admin')")]
    public UserRoles Role { get; set; } = UserRoles.Student;

    [Required]
    [Column("is_blocked", TypeName = "TINYINT(1)")]
    public bool IsBlocked { get; set; }

    [Required]
    [Column("is_verified", TypeName = "TINYINT(1)")]
    public bool IsVerified { get; set; }
}
