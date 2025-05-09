using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Noo.Api.Core.DataAbstraction.Model;
using Noo.Api.Core.DataAbstraction.Model.Attributes;
using Noo.Api.Works.DTO;
using Noo.Api.Works.Types;

namespace Noo.Api.Works.Models;

[Model("work")]
public class WorkModel : BaseModel
{
    [Required]
    [MinLength(1)]
    [MaxLength(200)]
    [Column("title", TypeName = "VARCHAR(200)")]
    [Index("work_title")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Column("type", TypeName = "VARCHAR(50)")]
    [Index("work_type")]
    public WorkType Type { get; set; }

    [MaxLength(255)]
    [Column("description", TypeName = "VARCHAR(255)")]
    public string? Description { get; set; }

    public ICollection<WorkTaskModel>? Tasks { get; set; }

    public WorkModel() { }

    public WorkModel(Ulid? id)
    {
        if (id != null)
        {
            Id = id.Value;
        }
    }
}
