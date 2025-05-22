using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction;
using Noo.Api.Core.DataAbstraction.Model;
using Noo.Api.Core.DataAbstraction.Model.Attributes;
using Noo.Api.Core.Utils.Richtext;
using Noo.Api.Works.Models;

namespace Noo.Api.Courses.Models;

[Model("course_material_content")]
public class CourseMaterialContentModel : BaseModel
{
    [RichTextColumn("content")]
    public IRichTextType? Content { get; set; }

    [Column("work_id", TypeName = DbDataTypes.Ulid)]
    [ForeignKey(nameof(Work))]
    public Ulid? WorkId { get; set; }

    [Column("is_work_available", TypeName = DbDataTypes.Boolean)]
    public bool IsWorkAvailable { get; set; }

    [Column("work_solve_deadline_at", TypeName = DbDataTypes.DateTimeWithoutTZ)]
    public DateTime? WorkSolveDeadlineAt { get; set; }

    [Column("work_check_deadline_at", TypeName = DbDataTypes.DateTimeWithoutTZ)]
    public DateTime? WorkCheckDeadlineAt { get; set; }

    #region Navigation properties

    [InverseProperty(nameof(WorkModel.CourseMaterialContents))]
    public WorkModel? Work { get; set; }

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public CourseMaterialModel Material { get; set; } = default!;

    #endregion
}
