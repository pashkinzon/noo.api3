using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Noo.Api.Core.DataAbstraction.Model;

public abstract class BaseModel
{
    [Key]
    [Column("id", TypeName = "BINARY(16)")]
    public Ulid Id { get; set; } = Ulid.NewUlid();

    [Required]
    [Column("created_at", TypeName = "TIMESTAMP(6)")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at", TypeName = "TIMESTAMP(6)")]
    public DateTime? UpdatedAt { get; set; }
}
