using System.ComponentModel.DataAnnotations;

namespace Noo.Api.Core.Config.Env;

public class EmailConfig : IConfig
{
    public static string SectionName => "Email";

    [Required]
    public string SmtpHost { get; set; } = string.Empty;

    [Required]
    [Range(1, 65535)]
    public int SmtpPort { get; set; } = 587;

    [Range(1, 15000)]
    public int SmtpTimeout { get; set; } = 10000;

    [Required]
    public string SmtpUsername { get; set; } = string.Empty;

    [Required]
    public string SmtpPassword { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string FromEmail { get; set; } = string.Empty;

    [Required]
    public string FromName { get; set; } = string.Empty;
}
