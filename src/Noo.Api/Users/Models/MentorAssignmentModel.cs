using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction.Model;
using Noo.Api.Core.DataAbstraction.Model.Attributes;
using Noo.Api.Subjects.Models;

namespace Noo.Api.Users.Models;

[Model("mentor_assignment")]
public class MentorAssignmentModel : BaseModel
{
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public UserModel Mentor { get; set; } = default!;

    [DeleteBehavior(DeleteBehavior.Cascade)]
    public UserModel Student { get; set; } = default!;

    [DeleteBehavior(DeleteBehavior.SetNull)]
    public SubjectModel Subject { get; set; } = default!;
}
