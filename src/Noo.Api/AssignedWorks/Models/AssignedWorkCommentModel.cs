using System.ComponentModel.DataAnnotations.Schema;
using Noo.Api.Core.DataAbstraction.Model;
using Noo.Api.Core.DataAbstraction.Model.Attributes;
using Noo.Api.Core.Utils.Richtext;

namespace Noo.Api.AssignedWorks.Models;

[Model("assigned_work_comment")]
public class AssignedWorkCommentModel : BaseModel
{
    [RichTextColumn("content")]
    public IRichTextType? Content { get; set; }
}
