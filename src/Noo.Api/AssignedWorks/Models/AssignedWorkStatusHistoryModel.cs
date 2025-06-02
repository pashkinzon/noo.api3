using System.ComponentModel.DataAnnotations.Schema;
using Noo.Api.AssignedWorks.Types;
using Noo.Api.Core.DataAbstraction;
using Noo.Api.Core.DataAbstraction.Model;
using Noo.Api.Core.DataAbstraction.Model.Attributes;

namespace Noo.Api.AssignedWorks.Models;

[Model("assigned_work_status_history")]
public class AssignedWorkStatusHistoryModel : BaseModel
{
    [Column("type", TypeName = "ENUM()")] // TODO: Define ENUM values
    public AssignedWorkStatusHistoryType Status { get; set; } = default!;

    [Column("changed_at", TypeName = DbDataTypes.DateTimeWithoutTZ)]
    public DateTime ChangedAt { get; set; }

    [Column("value", TypeName = DbDataTypes.Json)]
    public Dictionary<string, object>? Value { get; set; }

    #region Navigation Properties

    public AssignedWorkModel AssignedWork { get; set; } = default!;

    #endregion
}
