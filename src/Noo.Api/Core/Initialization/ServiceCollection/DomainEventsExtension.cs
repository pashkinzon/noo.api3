using Noo.Api.Core.System.Events;

namespace Noo.Api.Core.Initialization.ServiceCollection;

public static class DomainEventsExtension
{
    public static void AddDomainEventsBackgroundWorker(this IServiceCollection services)
    {
        services.AddHostedService<DomainEventDispatcher>();
    }
}
