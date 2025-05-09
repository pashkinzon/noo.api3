using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Noo.Api.Core.DataAbstraction.Model;

public abstract class OrderedModel : BaseModel
{
    [Required]
    [Column("order", TypeName = "INT")]
    public int Order { get; set; }
}
