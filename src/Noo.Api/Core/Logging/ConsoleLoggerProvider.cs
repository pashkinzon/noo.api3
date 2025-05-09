using Noo.Api.Core.Config.Env;

namespace Noo.Api.Core.Logging;

public class ConsoleLoggerProvider : ILoggerProvider
{
    private readonly LogConfig _options;

    public ConsoleLoggerProvider(LogConfig options)
    {
        _options = options;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new ConsoleLogger(categoryName, _options);
    }

    public void Dispose() { }
}
