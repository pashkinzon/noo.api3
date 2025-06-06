using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Noo.Api.Core.DataAbstraction;
using Noo.Api.Core.DataAbstraction.Model;
using Noo.Api.Core.DataAbstraction.Model.Attributes;
using Noo.Api.Core.Utils.Json;
using Noo.Api.Polls.Types;

namespace Noo.Api.Polls.Models;

[Model("poll_answer")]
public class PollAnswerModel : BaseModel
{
    [Column("poll_question_id", TypeName = DbDataTypes.Ulid)]
    [Required]
    [ForeignKey(nameof(PollQuestion))]
    public Ulid PollQuestionId { get; set; }

    [JsonColumn("value", Converter = typeof(PollAnswerValueConverter))]
    [Required]
    public PollAnswerValue Value { get; set; }

    #region Navigation Properties

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public PollQuestionModel PollQuestion { get; set; } = default!;

    #endregion
}
