using Noo.Api.Sessions.Background;

namespace Noo.Api.Core.Initialization.ServiceCollection;

public static class HostedServicesExtension
{
    public static void AddHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<SessionCleanupWorker>();
    }
}
