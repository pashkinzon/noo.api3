using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction.Model;
using Noo.Api.Core.DataAbstraction.Model.Attributes;
using Noo.Api.Core.Utils.Richtext;

namespace Noo.Api.AssignedWorks.Models;

[Model("assigned_work_comment")]
public class AssignedWorkCommentModel : BaseModel
{
    [RichTextColumn("content")]
    public IRichTextType? Content { get; set; }

    #region Navigation Properties

    [InverseProperty(nameof(AssignedWorkModel.MainMentorComment))]
    public AssignedWorkModel? AssignedWorkAsMainMentor { get; set; }

    [InverseProperty(nameof(AssignedWorkModel.HelperMentorComment))]
    public AssignedWorkModel? AssignedWorkAsHelperMentor { get; set; }

    [InverseProperty(nameof(AssignedWorkModel.StudentComment))]
    public AssignedWorkModel? AssignedWorkAsStudent { get; set; }


    #endregion
}
