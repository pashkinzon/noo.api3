using Noo.Api.Core.Utils.DI;

namespace Noo.Api.Core.System.Events;

[RegisterSingleton(typeof(IEventPublisher))]
public class InMemoryEventBus : IEventPublisher
{
    private readonly DomainEventQueue _queue;

    public InMemoryEventBus(DomainEventQueue queue)
    {
        _queue = queue;
    }

    public Task PublishAsync<TEvent>(TEvent @event, CancellationToken ct = default) where TEvent : IDomainEvent
    {
        _queue.TryEnqueue(@event);
        return Task.CompletedTask;
    }
}
