using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction.Model;
using Noo.Api.Core.DataAbstraction.Model.Attributes;
using Noo.Api.Works.Types;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace Noo.Api.Works.Models;

[Model("work")]
[Index(nameof(Title), IsUnique = false)]
[Index(nameof(Type), IsUnique = false)]
public class WorkModel : BaseModel
{
    [Required]
    [MinLength(1)]
    [MaxLength(200)]
    [Column("title", TypeName = "VARCHAR(200)")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Column("type", TypeName = "VARCHAR(50)")]
    public WorkType Type { get; set; }

    [MaxLength(255)]
    [Column("description", TypeName = "VARCHAR(255)")]
    public string? Description { get; set; }

    [InverseProperty(nameof(WorkTaskModel.Work))]
    public ICollection<WorkTaskModel>? Tasks { get; set; }

    #region Navigation Properties

    public WorkModel() { }

    #endregion

    public WorkModel(Ulid? id)
    {
        if (id != null)
        {
            Id = id.Value;
        }
    }
}
