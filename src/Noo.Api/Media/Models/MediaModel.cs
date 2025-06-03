using System.ComponentModel.DataAnnotations.Schema;
using Noo.Api.Core.DataAbstraction;
using Noo.Api.Core.DataAbstraction.Model;
using Noo.Api.Core.DataAbstraction.Model.Attributes;
using Noo.Api.Courses.Models;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace Noo.Api.Media.Models;

[Model("media")]
[Index(nameof(Hash), IsUnique = false)]
public class MediaModel : OrderedModel
{
    [Column("hash", TypeName = DbDataTypes.Varchar512)]
    public string Hash { get; set; } = string.Empty;

    [Column("path", TypeName = DbDataTypes.Varchar255)]
    public string Path { get; set; } = string.Empty;

    [Column("name", TypeName = DbDataTypes.Varchar255)]
    public string Name { get; set; } = string.Empty;

    [Column("actual_name", TypeName = DbDataTypes.Varchar255)]
    public string ActualName { get; set; } = string.Empty;

    [Column("extension", TypeName = DbDataTypes.Varchar127)]
    public string Extension { get; set; } = string.Empty;

    [Column("size", TypeName = DbDataTypes.Int)]
    public long Size { get; set; }

    #region Navigation Properties

    [InverseProperty(nameof(CourseModel.Thumbnail))]
    public ICollection<CourseModel> Courses { get; set; } = [];

    #endregion
}
