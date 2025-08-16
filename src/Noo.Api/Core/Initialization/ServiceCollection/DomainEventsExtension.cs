namespace Noo.Api.Core.Initialization.ServiceCollection;

public static class DomainEventsExtension
{
    public static Microsoft.Extensions.DependencyInjection.IServiceCollection AddDomainEventsBackgroundWorker(this Microsoft.Extensions.DependencyInjection.IServiceCollection services)
    {
        services.AddHostedService<Noo.Api.Core.System.Events.DomainEventDispatcher>();
        return services;
    }
}
