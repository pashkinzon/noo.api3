using Microsoft.AspNetCore.ResponseCompression;

namespace Noo.Api.Core.Initialization.ServiceCollection;

public static class ResponseCompressionExtension
{
    public static void AddNooResponseCompression(this IServiceCollection services)
    {
        services.AddResponseCompression(options => options.Providers.Add<BrotliCompressionProvider>());
    }
}
