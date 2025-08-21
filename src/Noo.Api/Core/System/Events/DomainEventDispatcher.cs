
namespace Noo.Api.Core.System.Events;

public class DomainEventDispatcher : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly DomainEventQueue _queue;

    public DomainEventDispatcher(IServiceProvider serviceProvider, DomainEventQueue queue)
    {
        _serviceProvider = serviceProvider;
        _queue = queue;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var reader = _queue.Reader;
        while (!stoppingToken.IsCancellationRequested)
        {
            IDomainEvent? next;
            try
            {
                next = await reader.ReadAsync(stoppingToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }

            try
            {
                await DispatchAsync(next, stoppingToken);
            }
            catch
            {
                // Swallow to keep the dispatcher alive; a structured logger could be injected here.
                await Task.Delay(TimeSpan.FromMilliseconds(50), stoppingToken);
            }
        }
    }

    private async Task DispatchAsync(IDomainEvent evt, CancellationToken ct)
    {
        using var scope = _serviceProvider.CreateScope();
        var eventType = evt.GetType();
        var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);
        var handlers = scope.ServiceProvider.GetServices(handlerType);

        foreach (var handler in handlers)
        {
            var method = handlerType.GetMethod("HandleAsync");
            if (method == null) continue;
            var task = (Task?)method.Invoke(handler, new object[] { evt, ct });
            if (task != null) await task.ConfigureAwait(false);
        }
    }
}
