using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Noo.Api.Core.DataAbstraction;
using Noo.Api.Core.DataAbstraction.Model;
using Noo.Api.Core.DataAbstraction.Model.Attributes;
using Noo.Api.Core.Utils.UserAgent;
using Noo.Api.Users.Models;

namespace Noo.Api.Sessions.Models;

[Model("session")]
public class SessionModel : BaseModel
{
    [Column("ip_address", TypeName = DbDataTypes.Varchar63)]
    [MaxLength(63)]
    public string? IpAddress { get; set; }

    [Column("user_agent", TypeName = DbDataTypes.Varchar255)]
    [MaxLength(255)]
    public string? UserAgent { get; set; }

    [Column("device", TypeName = DbDataTypes.Varchar255)]
    [MaxLength(255)]
    public string? Device { get; set; }

    [Column("device_type", TypeName = SessionEnumDbDataTypes.DeviceType)]
    public DeviceType DeviceType { get; set; } = DeviceType.Unknown;

    [Column("os", TypeName = DbDataTypes.Varchar255)]
    [MaxLength(255)]
    public string? Os { get; set; }

    [Column("browser", TypeName = DbDataTypes.Varchar255)]
    [MaxLength(255)]
    public string? Browser { get; set; }

    [Required]
    [Column("user_id", TypeName = DbDataTypes.Ulid)]
    [ForeignKey(nameof(User))]
    public Ulid UserId { get; set; }

    #region Navigation properties

    [InverseProperty(nameof(UserModel.Sessions))]
    public UserModel User { get; set; } = default!;

    #endregion
}
