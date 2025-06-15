using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Noo.Api.Core.DataAbstraction;
using Noo.Api.Core.DataAbstraction.Model;
using Noo.Api.Core.DataAbstraction.Model.Attributes;
using Noo.Api.Courses.Models;
using Noo.Api.Users.Models;
using Noo.Api.Works.Models;

namespace Noo.Api.Subjects.Models;

[Model("subject")]
public class SubjectModel : BaseModel
{
    [Required]
    [Column("name", TypeName = DbDataTypes.Varchar63)]
    [MinLength(1)]
    [MaxLength(63)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Column("color", TypeName = DbDataTypes.Varchar63)]
    [MinLength(1)]
    [MaxLength(63)]
    public string Color { get; set; } = string.Empty;

    #region Navigation Properties

    [InverseProperty(nameof(MentorAssignmentModel.Subject))]
    public ICollection<MentorAssignmentModel> MentorAssignments { get; set; } = [];

    [InverseProperty(nameof(WorkModel.Subject))]
    public ICollection<WorkModel> Works { get; set; } = [];

    [InverseProperty(nameof(CourseModel.Subject))]
    public ICollection<CourseModel> Courses { get; set; } = [];

    #endregion
}
