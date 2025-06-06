using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction;
using Noo.Api.Core.DataAbstraction.Model;
using Noo.Api.Core.DataAbstraction.Model.Attributes;
using Noo.Api.Core.Utils.Json;
using Noo.Api.Polls.Types;
using Noo.Api.Users.Models;

namespace Noo.Api.Polls.Models;

[Model("poll_participation")]
public class PollParticipationModel : BaseModel
{
    [Column("poll_id", TypeName = DbDataTypes.Ulid)]
    [Required]
    [ForeignKey(nameof(Poll))]
    public Ulid? PollId { get; set; }

    [Column("user_type", TypeName = PollEnumDbDataTypes.ParticipatingUserType)]
    [Required]
    public ParticipatingUserType UserType { get; set; } = default!;

    [Column("user_external_identifier", TypeName = DbDataTypes.Varchar255)]
    [MaxLength(255)]
    public string? UserExternalIdentifier { get; set; }

    [JsonColumn("user_external_data", Converter = typeof(PollUserExternalDataConverter))]
    public PollUserExternalData? UserExternalData { get; set; }

    [Column("user_id", TypeName = DbDataTypes.Ulid)]
    [ForeignKey(nameof(User))]
    public Ulid? UserId { get; set; }

    #region Navigation Properties

    [DeleteBehavior(DeleteBehavior.Cascade)]
    [InverseProperty(nameof(PollModel.Participations))]
    public PollModel Poll { get; set; } = default!;

    [DeleteBehavior(DeleteBehavior.Cascade)]
    [InverseProperty(nameof(UserModel.PollParticipations))]
    public UserModel? User { get; set; }

    public ICollection<PollAnswerModel> Answers { get; set; } = [];

    #endregion
}
