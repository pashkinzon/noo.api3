using System.ComponentModel.DataAnnotations;
using Noo.Api.Core.Security.Authentication;

namespace Noo.Api.Core.Config.Env;

public class AppConfig : IConfig
{
    public static string SectionName => "App";

    public required ApplicationMode Mode { get; set; }

    [Required]
    public required string Location { get; set; }

    [Required]
    public required string BaseUrl { get; set; }

    [Required]
    public readonly Version AppVersion = new(4, 0, 0);

    [Required]
    public readonly AuthenticationType AuthenticationType = AuthenticationType.Bearer;

    [Required]
    public required int UserOnlineThresholdMinutes { get; set; } = 15;

    [Required]
    public required int UserActiveThresholdDays { get; set; } = 14;

    public required string[] AllowedOrigins { get; set; } = [];
}
