using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction;
using Noo.Api.Core.DataAbstraction.Model;
using Noo.Api.Core.DataAbstraction.Model.Attributes;
using Noo.Api.NooTube.Types;
using Noo.Api.Users.Models;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace Noo.Api.NooTube.Models;

[Model("nootube_video_reaction")]
[Index(nameof(UserId), nameof(VideoId), IsUnique = true)]
public class NooTubeVideoReactionModel : BaseModel
{
    [Column("user_id", TypeName = DbDataTypes.Ulid)]
    [ForeignKey(nameof(User))]
    [Required]
    public Ulid UserId { get; set; }

    [Column("video_id", TypeName = DbDataTypes.Ulid)]
    [ForeignKey(nameof(Video))]
    [Required]
    public Ulid VideoId { get; set; }

    [Column("reaction", TypeName = NooTubeDbEnumDataTypes.VideoReaction)]
    public VideoReaction Reaction { get; set; } = default!;

    #region Navigation Properties

    [InverseProperty(nameof(UserModel.NooTubeVideoReactions))]
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public UserModel User { get; set; } = default!;

    [InverseProperty(nameof(NooTubeVideoModel.Reactions))]
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public NooTubeVideoModel Video { get; set; } = default!;

    #endregion
}
