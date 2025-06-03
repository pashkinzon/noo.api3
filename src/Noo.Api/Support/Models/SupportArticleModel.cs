using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction;
using Noo.Api.Core.DataAbstraction.Model;
using Noo.Api.Core.DataAbstraction.Model.Attributes;
using Noo.Api.Core.Utils.Richtext;

namespace Noo.Api.Support.Models;

[Model("support_article")]
public class SupportArticleModel : OrderedModel
{
    [Column("title", TypeName = DbDataTypes.Varchar255)]
    [Required]
    [MaxLength(255)]
    public string Title { get; set; } = string.Empty;

    [RichTextColumn("content")]
    public IRichTextType Content { get; set; } = default!;

    [Column("is_pinned", TypeName = DbDataTypes.Boolean)]
    public bool IsPinned { get; set; }

    #region Navigation Properties

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public SupportCategoryModel Category { get; set; } = default!;

    #endregion
}
