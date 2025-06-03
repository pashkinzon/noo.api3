using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction;
using Noo.Api.Core.DataAbstraction.Model;
using Noo.Api.Core.DataAbstraction.Model.Attributes;

namespace Noo.Api.Support.Models;

[Model("support_category")]
public class SupportCategoryModel : OrderedModel
{
    [Column("name", TypeName = DbDataTypes.Varchar255)]
    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [Column("is_pinned", TypeName = DbDataTypes.Boolean)]
    public bool IsPinned { get; set; }

    [Column("is_active", TypeName = DbDataTypes.Boolean)]
    public bool IsActive { get; set; } = true;

    [Column("parent_id", TypeName = DbDataTypes.Ulid)]
    [ForeignKey(nameof(Parent))]
    public Ulid? ParentId { get; set; }

    #region Navigation Properties

    [InverseProperty(nameof(Parent))]
    public ICollection<SupportCategoryModel> Children { get; set; } = [];

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public SupportCategoryModel? Parent { get; set; }

    public ICollection<SupportArticleModel> Articles { get; set; } = [];

    #endregion
}
