using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction.Model;
using Noo.Api.Core.DataAbstraction.Model.Attributes;
using Noo.Api.Core.Utils.Richtext;
using Noo.Api.Works.Types;

namespace Noo.Api.Works.Models;

[Model("work_task")]
public class WorkTaskModel : OrderedModel
{
    [RichTextColumn("content")]
    [Required]
    public IRichTextType? Content { get; set; }

    [RichTextColumn("solve_hint")]
    public IRichTextType? SolveHint { get; set; }

    [RichTextColumn("explanation")]
    public IRichTextType? Explanation { get; set; }

    [Column("right_answer", TypeName = "VARCHAR(255)")]
    [MaxLength(255)]
    public string? RightAnswer { get; set; }

    [Column("type", TypeName = "VARCHAR(50)")]
    [Required]
    public WorkTaskType Type { get; set; }

    [Column("check_strategy", TypeName = "VARCHAR(50)")]
    [Required]
    public WorkTaskCheckStrategy CheckStrategy { get; set; } = WorkTaskCheckStrategy.Manual;

    [Column("max_score", TypeName = "INT")]
    [Required]
    [Range(0, int.MaxValue)]
    public int MaxScore { get; set; }

    [Column("show_answer_before_check", TypeName = "TINYINT(1)")]
    public bool ShowAnswerBeforeCheck { get; set; } = false;

    [Column("check_one_by_one", TypeName = "TINYINT(1)")]
    public bool CheckOneByOne { get; set; } = false;

    [Column("work_id", TypeName = "BINARY(16)")]
    [ForeignKey(nameof(Work))]
    public Ulid WorkId { get; set; }

    #region Navigation Properties

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public WorkModel? Work { get; set; }

    #endregion
}
