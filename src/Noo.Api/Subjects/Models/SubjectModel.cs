using Noo.Api.Core.DataAbstraction.Model;
using Noo.Api.Core.DataAbstraction.Model.Attributes;
using Noo.Api.Users.Models;
using Noo.Api.Works.Models;

namespace Noo.Api.Subjects.Models;

[Model("subject")]
public class SubjectModel : BaseModel
{
    public string Name { get; set; } = string.Empty;

    public string Color { get; set; } = string.Empty;

    #region Navigation Properties

    public ICollection<MentorAssignmentModel> MentorAssignments { get; set; } = [];

    public ICollection<WorkModel> Works { get; set; } = [];

    #endregion
}
