namespace Noo.Api.Core.Config;

public class MissingConfigException<T> : Exception where T : IConfig
{
    public MissingConfigException() : base($"Missing configuration for {typeof(T).Name}")
    {
    }

    public MissingConfigException(string? message) : base(message)
    {
    }

    public MissingConfigException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
