using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction;
using Noo.Api.Core.DataAbstraction.Model;
using Noo.Api.Core.DataAbstraction.Model.Attributes;
using Noo.Api.Users.Models;

namespace Noo.Api.NooTube.Models;

[Model("nootube_video_comment")]
public class NooTubeVideoCommentModel : BaseModel
{
    [Column("video_id", TypeName = DbDataTypes.Ulid)]
    [ForeignKey(nameof(Video))]
    [Required]
    public Ulid VideoId { get; set; }

    [Column("user_id", TypeName = DbDataTypes.Ulid)]
    [ForeignKey(nameof(User))]
    [Required]
    public Ulid UserId { get; set; }

    [Column("content", TypeName = DbDataTypes.Varchar512)]
    [Required]
    [MaxLength(512)]
    public string Content { get; set; } = string.Empty;

    #region Navigation Properties

    [InverseProperty(nameof(NooTubeVideoModel.Comments))]
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public NooTubeVideoModel Video { get; set; } = default!;

    [InverseProperty(nameof(UserModel.NooTubeVideoComments))]
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public UserModel User { get; set; } = default!;

    #endregion
}
