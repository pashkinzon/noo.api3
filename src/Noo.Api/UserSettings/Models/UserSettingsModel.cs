using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction;
using Noo.Api.Core.DataAbstraction.Model;
using Noo.Api.Core.DataAbstraction.Model.Attributes;
using Noo.Api.Users.Models;

namespace Noo.Api.UserSettings.Models;

[Model("user_settings")]
public class UserSettingsModel : BaseModel
{
    [Column("user_id", TypeName = DbDataTypes.Ulid)]
    [ForeignKey(nameof(User))]
    [Required]
    public Ulid UserId { get; set; }

    [Column("theme")]
    public string? Theme { get; set; }

    [Column("font_size")]
    public string? FontSize { get; set; }

    #region Navigation Properties

    [InverseProperty(nameof(UserModel.Settings))]
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public UserModel User { get; set; } = default!;

    #endregion
}
