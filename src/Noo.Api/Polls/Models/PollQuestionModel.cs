using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction;
using Noo.Api.Core.DataAbstraction.Model;
using Noo.Api.Core.DataAbstraction.Model.Attributes;
using Noo.Api.Polls.Types;

namespace Noo.Api.Polls.Models;

[Model("poll_question")]
public class PollQuestionModel : OrderedModel
{
    [Column("poll_id", TypeName = DbDataTypes.Ulid)]
    [Required]
    [ForeignKey(nameof(Poll))]
    public string PollId { get; set; } = string.Empty;

    [Column("title", TypeName = DbDataTypes.Varchar255)]
    [Required]
    [MaxLength(255)]
    public string Title { get; set; } = string.Empty;

    [Column("description", TypeName = DbDataTypes.Varchar512)]
    [MaxLength(512)]
    public string? Description { get; set; }

    [Column("is_required", TypeName = DbDataTypes.Boolean)]
    [Required]
    public bool IsRequired { get; set; }

    [Column("type", TypeName = PollEnumDbDataTypes.PollQuestionType)]
    [Required]
    public PollQuestionType Type { get; set; }

    [Column("config", TypeName = DbDataTypes.Json)]
    public PollQuestionConfig? Config { get; set; }

    #region Navigation Properties

    [DeleteBehavior(DeleteBehavior.Cascade)]
    [InverseProperty(nameof(PollModel.Questions))]
    public PollModel Poll { get; set; } = default!;

    public ICollection<PollAnswerModel> Answers { get; set; } = [];

    #endregion
}
