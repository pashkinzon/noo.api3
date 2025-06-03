using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

    #region Navigation Properties

    public ICollection<SupportCategoryModel> Children { get; set; } = [];

    public ICollection<SupportArticleModel> Articles { get; set; } = [];

    #endregion
}
