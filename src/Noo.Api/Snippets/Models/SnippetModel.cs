using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction;
using Noo.Api.Core.DataAbstraction.Model;
using Noo.Api.Core.DataAbstraction.Model.Attributes;
using Noo.Api.Core.Utils.Richtext;
using Noo.Api.Users.Models;

namespace Noo.Api.Snippets.Models;

[Model("snippet")]
public class SnippetModel : BaseModel
{
    [Column("name", TypeName = DbDataTypes.Varchar63)]
    [MaxLength(63)]
    public string Name { get; set; } = default!;

    [RichTextColumn("content")]
    public string? Content { get; set; }

    [Column("user_id", TypeName = DbDataTypes.Ulid)]
    [ForeignKey(nameof(User))]
    public Ulid UserId { get; set; }

    #region Navigation Properties

    [InverseProperty(nameof(User.Snippets))]
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public UserModel User { get; set; } = default!;

    #endregion
}
