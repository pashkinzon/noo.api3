using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Noo.Api.AssignedWorks.Models;
using Noo.Api.Calendar.Types;
using Noo.Api.Core.DataAbstraction;
using Noo.Api.Core.DataAbstraction.Model;
using Noo.Api.Core.DataAbstraction.Model.Attributes;
using Noo.Api.Users.Models;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace Noo.Api.Calendar.Models;

[Model("calendar_event")]
[Index(nameof(DateTime), IsUnique = false)]
public class CalendarEventModel : BaseModel
{
    [Column("user_id", TypeName = DbDataTypes.Ulid)]
    [Required]
    [ForeignKey(nameof(User))]
    public Ulid UserId { get; set; }

    [Column("assigned_work_id", TypeName = DbDataTypes.Ulid)]
    [ForeignKey(nameof(AssignedWork))]
    public Ulid? AssignedWorkId { get; set; }

    [Column("type", TypeName = CalendarEnumDbDataTypes.CalendarEventType)]
    [Required]
    public CalendarEventType Type { get; set; } = CalendarEventType.Custom;

    [Column("title", TypeName = DbDataTypes.Varchar255)]
    [Required]
    [MaxLength(255)]
    public string Title { get; set; } = string.Empty;

    [Column("description", TypeName = DbDataTypes.Varchar512)]
    [MaxLength(512)]
    public string? Description { get; set; }

    [Column("datetime", TypeName = DbDataTypes.DateTimeWithoutTZ)]
    [Required]
    public DateTime DateTime { get; set; }

    #region Navigation Properties

    [DeleteBehavior(DeleteBehavior.Cascade)]
    [InverseProperty(nameof(UserModel.CalendarEvents))]
    public UserModel User { get; set; } = default!;

    [DeleteBehavior(DeleteBehavior.Cascade)]
    [InverseProperty(nameof(AssignedWorkModel.Events))]
    public AssignedWorkModel? AssignedWork { get; set; }

    #endregion
}
