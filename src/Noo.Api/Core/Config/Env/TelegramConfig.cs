using System.ComponentModel.DataAnnotations;

namespace Noo.Api.Core.Config.Env;

public class TelegramConfig : IConfig
{
    public static string SectionName => "Telegram";

    [Required]
    public required string Token { get; set; }

    public int WaitAfterBatch { get; set; } = 100;

    public int WaitForBatch { get; set; } = 1000;
}
