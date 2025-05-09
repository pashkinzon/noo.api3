using Microsoft.Extensions.Logging.Console;
using Noo.Api.Core.Config.Env;
using Noo.Api.Core.Initialization.Configuration;

namespace Noo.Api.Core.Initialization.ServiceCollection;

public static class LoggingExtension
{
    public static void AddLogger(this IServiceCollection services, IConfiguration config)
    {
        var logConfig = config.GetSection(LogConfig.SectionName).GetOrThrow<LogConfig>();

        services.AddLogging(loggingBuilder =>
        {
            switch (logConfig.Mode)
            {
                case Config.LogMode.Console:
                    loggingBuilder.AddConsole(options =>
                    {
                        options.FormatterName = ConsoleFormatterNames.Systemd;
                        options.LogToStandardErrorThreshold = logConfig.MinLevel;
                    });
                    break;
                case Config.LogMode.Telegram:
                default:
                    throw new NotSupportedException($"Log mode {logConfig.Mode} is not supported.");
            }
        });
    }
}
