using Microsoft.AspNetCore.Server.Kestrel.Core;
using Noo.Api.Core.Config.Env;
using Noo.Api.Core.Initialization.Configuration;

namespace Noo.Api.Core.Initialization.WebHostBuilder;

public static class WebServerConfigurationExtension
{
    public static void AddWebServerConfiguration(this ConfigureWebHostBuilder webhost, IConfiguration configuration)
    {
        var httpConfig = configuration.GetSection(HttpConfig.SectionName).GetOrThrow<HttpConfig>();

        webhost.ConfigureKestrel(options =>
        {
            options.Limits.MaxRequestBodySize = httpConfig.MaximumRequestSize;
            options.AddServerHeader = false;

            options.Limits.MinRequestBodyDataRate = new MinDataRate(
                bytesPerSecond: httpConfig.MinRequestBodyDataRate,
                gracePeriod: TimeSpan.FromSeconds(httpConfig.MinRequestBodyDataRateGracePeriod)
            );

            options.Limits.MinResponseDataRate = new MinDataRate(
                bytesPerSecond: httpConfig.MinResponseDataRate,
                gracePeriod: TimeSpan.FromSeconds(httpConfig.MinResponseDataRateGracePeriod)
            );

            options.ListenAnyIP(httpConfig.Port, listenOptions => listenOptions.Protocols = HttpProtocols.Http1AndHttp2);
        });
    }
}
