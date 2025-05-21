using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction.Model;
using Noo.Api.Core.DataAbstraction.Model.Attributes;
using Noo.Api.Subjects.Models;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace Noo.Api.Users.Models;

[Model("mentor_assignment")]
[Index(nameof(StudentId), nameof(MentorId), nameof(SubjectId), IsUnique = true)]
public class MentorAssignmentModel : BaseModel
{
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public UserModel Mentor { get; set; } = default!;

    [Column("mentor_id", TypeName = "BINARY(16)")]
    [ForeignKey(nameof(Mentor))]
    public Ulid MentorId { get; set; }

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public UserModel Student { get; set; } = default!;

    [Column("student_id", TypeName = "BINARY(16)")]
    [ForeignKey(nameof(Student))]
    public Ulid StudentId { get; set; }

    [DeleteBehavior(DeleteBehavior.SetNull)]
    public SubjectModel? Subject { get; set; }

    [Column("subject_id", TypeName = "BINARY(16)")]
    [ForeignKey(nameof(Subject))]
    public Ulid? SubjectId { get; set; }
}
