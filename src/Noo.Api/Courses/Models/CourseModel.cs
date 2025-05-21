using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction.Model;
using Noo.Api.Core.DataAbstraction.Model.Attributes;
using Noo.Api.Users.Models;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace Noo.Api.Courses.Models;

[Model("course")]
[Index(nameof(Name), IsUnique = false)]
public class CourseModel : BaseModel
{
    [Required]
    [Column("name", TypeName = "varchar(255)")]
    public string Name { get; set; } = string.Empty;

    [Column("description", TypeName = "text")]
    [MaxLength(500)]
    public string? Description { get; set; }

    // TODO: Add media table
    // [Column("thumbnail_id", TypeName = "BINARY(16)")]
    // [ForeignKey(nameof(Thumbnail))]
    // public Ulid? ThumbnailId { get; set; }

    #region Navigation Properties

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public ICollection<CourseChapterModel> Chapters { get; set; } = [];

    [DeleteBehavior(DeleteBehavior.SetNull)]
    public ICollection<UserModel> Editors { get; set; } = [];

    [DeleteBehavior(DeleteBehavior.SetNull)]
    public ICollection<UserModel> Authors { get; set; } = [];

    [DeleteBehavior(DeleteBehavior.SetNull)]
    public ICollection<CourseAssignmentModel> Assignments { get; set; } = [];

    #endregion
}
