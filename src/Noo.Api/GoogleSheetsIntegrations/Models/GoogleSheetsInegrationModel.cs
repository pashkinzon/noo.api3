using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Noo.Api.Core.DataAbstraction;
using Noo.Api.Core.DataAbstraction.Model;
using Noo.Api.Core.DataAbstraction.Model.Attributes;
using Noo.Api.Core.Utils.Json;
using Noo.Api.GoogleSheetsIntegrations.Types;

namespace Noo.Api.GoogleSheetsIntegrations.Models;

[Model("google_sheets_integration")]
public class GoogleSheetsInegrationModel : BaseModel
{
    [Column("name", TypeName = DbDataTypes.Varchar255)]
    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [Column("entity", TypeName = DbDataTypes.Varchar255)]
    [Required]
    public string Entity { get; set; } = string.Empty;

    [Column("last_run_at", TypeName = DbDataTypes.DateTimeWithoutTZ)]
    public DateTime? LastRunAt { get; set; }

    [Column("status", TypeName = GoogleSheetsIntegrationEnumDataDbTypes.GoogleSheetsIntegrationStatus)]
    [Required]
    public GoogleSheetsIntegrationStatus Status { get; set; } = GoogleSheetsIntegrationStatus.Active;

    [Column("last_error_text", TypeName = DbDataTypes.Text)]
    public string? LastErrorText { get; set; }

    [Column("cron_pattern", TypeName = DbDataTypes.Varchar63)]
    [Required]
    [MaxLength(63)]
    public string CronPattern { get; set; } = string.Empty;

    [JsonColumn("google_auth_data", Converter = typeof(GoogleSheetsAuthDataConverter))]
    [Required]
    public GoogleSheetsAuthData GoogleAuthData { get; set; } = default!;
}
