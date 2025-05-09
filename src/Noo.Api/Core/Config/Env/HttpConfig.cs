namespace Noo.Api.Core.Config.Env;

public class HttpConfig : IConfig
{
    public static string SectionName => "Http";

    public int Port { get; set; } = 5000;

    public int MaximumRequestSize { get; set; } = 10 * 1024 * 1024;

    public int MaximumCacheBodySize { get; set; } = 1024 * 1024;

    public int CacheSizeLimit { get; set; } = 100 * 1024 * 1024;

    public int MinRequestBodyDataRate { get; set; } = 100;

    public int MinRequestBodyDataRateGracePeriod { get; set; } = 10;

    public int MinResponseDataRate { get; set; } = 100;

    public int MinResponseDataRateGracePeriod { get; set; } = 10;
}
